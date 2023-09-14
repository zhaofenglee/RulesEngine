using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using Volo.Abp.BlazoriseUI.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using JS.Abp.RulesEngine.RulesMembers;
using JS.Abp.RulesEngine.Permissions;
using JS.Abp.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.Shared;
using Microsoft.AspNetCore.Components;

namespace JS.Abp.RulesEngine.Blazor.Pages.RulesEngine
{
    public partial class RulesMembers
    {
        [Parameter] public Guid RulesGroupId { get; set; }
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        protected bool ShowAdvancedFilters { get; set; }
        private IReadOnlyList<RulesMemberWithNavigationPropertiesDto> RulesMemberList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateRulesMember { get; set; }
        private bool CanEditRulesMember { get; set; }
        private bool CanDeleteRulesMember { get; set; }
        private RulesMemberCreateDto NewRulesMember { get; set; }
        private Validations NewRulesMemberValidations { get; set; } = new();
        private RulesMemberUpdateDto EditingRulesMember { get; set; }
        private Validations EditingRulesMemberValidations { get; set; } = new();
        private Guid EditingRulesMemberId { get; set; }
        private Modal CreateRulesMemberModal { get; set; } = new();
        private Modal EditRulesMemberModal { get; set; } = new();
        private GetRulesMembersInput Filter { get; set; }
        private DataGridEntityActionsColumn<RulesMemberWithNavigationPropertiesDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "rulesMember-create-tab";
        protected string SelectedEditTab = "rulesMember-edit-tab";
        private IReadOnlyList<LookupDto<Guid>> RulesGroupsCollection { get; set; } = new List<LookupDto<Guid>>();
        private IReadOnlyList<LookupDto<Guid>> RulesCollection { get; set; } = new List<LookupDto<Guid>>();
        private RulesGroupUpdateDto RulesGroup { get; set; }
        public RulesMembers()
        {
            RulesGroup = new RulesGroupUpdateDto();
            NewRulesMember = new RulesMemberCreateDto();
            EditingRulesMember = new RulesMemberUpdateDto();
            Filter = new GetRulesMembersInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            RulesMemberList = new List<RulesMemberWithNavigationPropertiesDto>();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
            await GetRulesGroupCollectionLookupAsync();


            await GetRuleCollectionLookupAsync();
            await GetRulesGroupAsync();

        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:RulesGroups"],"/RulesEngine/RulesGroups"));
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:RulesMembers"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            //Toolbar.AddButton(L["ExportToExcel"], async () =>{ await DownloadAsExcelAsync(); }, IconName.Download);
            
            // Toolbar.AddButton(L["NewRulesMember"], async () =>
            // {
            //     await OpenCreateRulesMemberModalAsync();
            // }, IconName.Add, requiredPolicyName: RulesEnginePermissions.RulesMembers.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateRulesMember = await AuthorizationService
                .IsGrantedAsync(RulesEnginePermissions.RulesMembers.Create);
            CanEditRulesMember = await AuthorizationService
                            .IsGrantedAsync(RulesEnginePermissions.RulesMembers.Edit);
            CanDeleteRulesMember = await AuthorizationService
                            .IsGrantedAsync(RulesEnginePermissions.RulesMembers.Delete);
        }

        private async Task GetRulesMembersAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;
            Filter.RulesGroupId = RulesGroupId;

            var result = await RulesMembersAppService.GetListAsync(Filter);
            RulesMemberList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        private async Task GetRulesGroupAsync()
        {
            if (RulesGroupId!=Guid.Empty)
            {
                var  rulesGroup = await RulesGroupsAppService.GetAsync(RulesGroupId);
            
                RulesGroup = ObjectMapper.Map<RulesGroupDto, RulesGroupUpdateDto>(rulesGroup);
            }
          
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetRulesMembersAsync();
            await InvokeAsync(StateHasChanged);
        }

        private  async Task DownloadAsExcelAsync()
        {
            var token = (await RulesMembersAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("RulesEngine") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/rules-engine/rules-members/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<RulesMemberWithNavigationPropertiesDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetRulesMembersAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateRulesMemberModalAsync()
        {
            NewRulesMember = new RulesMemberCreateDto{
                RulesGroupId = RulesGroupId,
                
            };
            await NewRulesMemberValidations.ClearAll();
            await CreateRulesMemberModal.Show();
        }

        private async Task CloseCreateRulesMemberModalAsync()
        {
            NewRulesMember = new RulesMemberCreateDto{
                
                
            };
            await CreateRulesMemberModal.Hide();
        }

        private async Task OpenEditRulesMemberModalAsync(RulesMemberWithNavigationPropertiesDto input)
        {
            var rulesMember = await RulesMembersAppService.GetWithNavigationPropertiesAsync(input.RulesMember.Id);
            
            EditingRulesMemberId = rulesMember.RulesMember.Id;
            EditingRulesMember = ObjectMapper.Map<RulesMemberDto, RulesMemberUpdateDto>(rulesMember.RulesMember);
            await EditingRulesMemberValidations.ClearAll();
            await EditRulesMemberModal.Show();
        }

        private async Task DeleteRulesMemberAsync(RulesMemberWithNavigationPropertiesDto input)
        {
            await RulesMembersAppService.DeleteAsync(input.RulesMember.Id);
            await GetRulesMembersAsync();
        }

        private async Task CreateRulesMemberAsync()
        {
            try
            {
                if (await NewRulesMemberValidations.ValidateAll() == false)
                {
                    return;
                }

                await RulesMembersAppService.CreateAsync(NewRulesMember);
                await GetRulesMembersAsync();
                await CloseCreateRulesMemberModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditRulesMemberModalAsync()
        {
            await EditRulesMemberModal.Hide();
        }

        private async Task UpdateRulesMemberAsync()
        {
            try
            {
                if (await EditingRulesMemberValidations.ValidateAll() == false)
                {
                    return;
                }

                await RulesMembersAppService.UpdateAsync(EditingRulesMemberId, EditingRulesMember);
                await GetRulesMembersAsync();
                await EditRulesMemberModal.Hide();                
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private void OnSelectedCreateTabChanged(string name)
        {
            SelectedCreateTab = name;
        }

        private void OnSelectedEditTabChanged(string name)
        {
            SelectedEditTab = name;
        }

        protected virtual async Task OnSequenceMinChangedAsync(int? sequenceMin)
        {
            Filter.SequenceMin = sequenceMin;
            await SearchAsync();
        }
        protected virtual async Task OnSequenceMaxChangedAsync(int? sequenceMax)
        {
            Filter.SequenceMax = sequenceMax;
            await SearchAsync();
        }
        protected virtual async Task OnDescriptionChangedAsync(string? description)
        {
            Filter.Description = description;
            await SearchAsync();
        }
        protected virtual async Task OnRulesGroupIdChangedAsync(Guid? rulesGroupId)
        {
            Filter.RulesGroupId = rulesGroupId;
            await SearchAsync();
        }
        protected virtual async Task OnRuleIdChangedAsync(Guid? ruleId)
        {
            Filter.RuleId = ruleId;
            await SearchAsync();
        }
        

        private async Task GetRulesGroupCollectionLookupAsync(string? newValue = null)
        {
            RulesGroupsCollection = (await RulesMembersAppService.GetRulesGroupLookupAsync(new LookupRequestDto { Filter = newValue })).Items;
        }

        private async Task GetRuleCollectionLookupAsync(string? newValue = null)
        {
            RulesCollection = (await RulesMembersAppService.GetRuleLookupAsync(new LookupRequestDto { Filter = newValue })).Items;
        }

    }
}
