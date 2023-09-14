using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using JS.Abp.RulesEngine.OperatorTypes;
using Volo.Abp.BlazoriseUI.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using JS.Abp.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.Permissions;
using JS.Abp.RulesEngine.Shared;

namespace JS.Abp.RulesEngine.Blazor.Pages.RulesEngine
{
    public partial class RulesGroups
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        protected bool ShowAdvancedFilters { get; set; }
        private IReadOnlyList<RulesGroupDto> RulesGroupList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateRulesGroup { get; set; }
        private bool CanEditRulesGroup { get; set; }
        private bool CanDeleteRulesGroup { get; set; }
        private RulesGroupCreateDto NewRulesGroup { get; set; }
        private Validations NewRulesGroupValidations { get; set; } = new();
        private RulesGroupUpdateDto EditingRulesGroup { get; set; }
        private Validations EditingRulesGroupValidations { get; set; } = new();
        private Guid EditingRulesGroupId { get; set; }
        private Modal CreateRulesGroupModal { get; set; } = new();
        private Modal EditRulesGroupModal { get; set; } = new();
        private GetRulesGroupsInput Filter { get; set; }
        private DataGridEntityActionsColumn<RulesGroupDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "rulesGroup-create-tab";
        protected string SelectedEditTab = "rulesGroup-edit-tab";
        
        public RulesGroups()
        {
            NewRulesGroup = new RulesGroupCreateDto();
            EditingRulesGroup = new RulesGroupUpdateDto();
            Filter = new GetRulesGroupsInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            RulesGroupList = new List<RulesGroupDto>();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:RulesGroups"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () =>{ await DownloadAsExcelAsync(); }, IconName.Download);
            
            Toolbar.AddButton(L["NewRulesGroup"], async () =>
            {
                await OpenCreateRulesGroupModalAsync();
            }, IconName.Add, requiredPolicyName: RulesEnginePermissions.RulesGroups.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateRulesGroup = await AuthorizationService
                .IsGrantedAsync(RulesEnginePermissions.RulesGroups.Create);
            CanEditRulesGroup = await AuthorizationService
                            .IsGrantedAsync(RulesEnginePermissions.RulesGroups.Edit);
            CanDeleteRulesGroup = await AuthorizationService
                            .IsGrantedAsync(RulesEnginePermissions.RulesGroups.Delete);
        }

        private async Task GetRulesGroupsAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await RulesGroupsAppService.GetListAsync(Filter);
            RulesGroupList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetRulesGroupsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private  async Task DownloadAsExcelAsync()
        {
            var token = (await RulesGroupsAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("RulesEngine") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/rules-engine/rules-groups/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<RulesGroupDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetRulesGroupsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateRulesGroupModalAsync()
        {
            NewRulesGroup = new RulesGroupCreateDto{
                
                
            };
            await NewRulesGroupValidations.ClearAll();
            await CreateRulesGroupModal.Show();
        }

        private async Task CloseCreateRulesGroupModalAsync()
        {
            NewRulesGroup = new RulesGroupCreateDto{
                
                
            };
            await CreateRulesGroupModal.Hide();
        }

        private async Task OpenRulesMembersAsync(RulesGroupDto input)
        {
            var detailUrl = $"/RulesEngine/RulesGroups/{input.Id}";
            NavigationManager.NavigateTo(detailUrl);
        }

        private async Task OpenEditRulesGroupModalAsync(RulesGroupDto input)
        {
            var rulesGroup = await RulesGroupsAppService.GetAsync(input.Id);
            
            EditingRulesGroupId = rulesGroup.Id;
            EditingRulesGroup = ObjectMapper.Map<RulesGroupDto, RulesGroupUpdateDto>(rulesGroup);
            await EditingRulesGroupValidations.ClearAll();
            await EditRulesGroupModal.Show();
        }

        private async Task DeleteRulesGroupAsync(RulesGroupDto input)
        {
            await RulesGroupsAppService.DeleteAsync(input.Id);
            await GetRulesGroupsAsync();
        }

        private async Task CreateRulesGroupAsync()
        {
            try
            {
                if (await NewRulesGroupValidations.ValidateAll() == false)
                {
                    return;
                }

                await RulesGroupsAppService.CreateAsync(NewRulesGroup);
                await GetRulesGroupsAsync();
                await CloseCreateRulesGroupModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditRulesGroupModalAsync()
        {
            await EditRulesGroupModal.Hide();
        }

        private async Task UpdateRulesGroupAsync()
        {
            try
            {
                if (await EditingRulesGroupValidations.ValidateAll() == false)
                {
                    return;
                }

                await RulesGroupsAppService.UpdateAsync(EditingRulesGroupId, EditingRulesGroup);
                await GetRulesGroupsAsync();
                await EditRulesGroupModal.Hide();                
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

        protected virtual async Task OnGroupNameChangedAsync(string? groupName)
        {
            Filter.GroupName = groupName;
            await SearchAsync();
        }
        protected virtual async Task OnOperatorTypeChangedAsync(OperatorType? operatorType)
        {
            Filter.OperatorType = operatorType;
            await SearchAsync();
        }
        protected virtual async Task OnDescriptionChangedAsync(string? description)
        {
            Filter.Description = description;
            await SearchAsync();
        }
        

    }
}
