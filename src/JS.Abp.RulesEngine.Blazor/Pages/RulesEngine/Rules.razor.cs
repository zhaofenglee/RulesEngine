using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using JS.Abp.RulesEngine.ErrorTypes;
using Volo.Abp.BlazoriseUI.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using JS.Abp.RulesEngine.Rules;
using JS.Abp.RulesEngine.Permissions;
using JS.Abp.RulesEngine.RuleExpressionTypes;
using JS.Abp.RulesEngine.Shared;

namespace JS.Abp.RulesEngine.Blazor.Pages.RulesEngine
{
    public partial class Rules
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        protected bool ShowAdvancedFilters { get; set; }
        private IReadOnlyList<RuleDto> RuleList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateRule { get; set; }
        private bool CanEditRule { get; set; }
        private bool CanDeleteRule { get; set; }
        private RuleCreateDto NewRule { get; set; }
        private Validations NewRuleValidations { get; set; } = new();
        private RuleUpdateDto EditingRule { get; set; }
        private Validations EditingRuleValidations { get; set; } = new();
        private Guid EditingRuleId { get; set; }
        private Modal CreateRuleModal { get; set; } = new();
        private Modal EditRuleModal { get; set; } = new();
        private GetRulesInput Filter { get; set; }
        private DataGridEntityActionsColumn<RuleDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "rule-create-tab";
        protected string SelectedEditTab = "rule-edit-tab";
        
        public Rules()
        {
            NewRule = new RuleCreateDto();
            EditingRule = new RuleUpdateDto();
            Filter = new GetRulesInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            RuleList = new List<RuleDto>();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:Rules"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () =>{ await DownloadAsExcelAsync(); }, IconName.Download);
            
            Toolbar.AddButton(L["NewRule"], async () =>
            {
                await OpenCreateRuleModalAsync();
            }, IconName.Add, requiredPolicyName: RulesEnginePermissions.Rules.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateRule = await AuthorizationService
                .IsGrantedAsync(RulesEnginePermissions.Rules.Create);
            CanEditRule = await AuthorizationService
                            .IsGrantedAsync(RulesEnginePermissions.Rules.Edit);
            CanDeleteRule = await AuthorizationService
                            .IsGrantedAsync(RulesEnginePermissions.Rules.Delete);
        }

        private async Task GetRulesAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await RulesAppService.GetListAsync(Filter);
            RuleList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetRulesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private  async Task DownloadAsExcelAsync()
        {
            var token = (await RulesAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("RulesEngine") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/rules-engine/rules/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<RuleDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetRulesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateRuleModalAsync()
        {
            NewRule = new RuleCreateDto{
                
                
            };
            await NewRuleValidations.ClearAll();
            await CreateRuleModal.Show();
        }

        private async Task CloseCreateRuleModalAsync()
        {
            NewRule = new RuleCreateDto{
                
                
            };
            await CreateRuleModal.Hide();
        }

        private async Task OpenEditRuleModalAsync(RuleDto input)
        {
            var rule = await RulesAppService.GetAsync(input.Id);
            
            EditingRuleId = rule.Id;
            EditingRule = ObjectMapper.Map<RuleDto, RuleUpdateDto>(rule);
            await EditingRuleValidations.ClearAll();
            await EditRuleModal.Show();
        }

        private async Task DeleteRuleAsync(RuleDto input)
        {
            await RulesAppService.DeleteAsync(input.Id);
            await GetRulesAsync();
        }

        private async Task CreateRuleAsync()
        {
            try
            {
                if (await NewRuleValidations.ValidateAll() == false)
                {
                    return;
                }

                await RulesAppService.CreateAsync(NewRule);
                await GetRulesAsync();
                await CloseCreateRuleModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditRuleModalAsync()
        {
            await EditRuleModal.Hide();
        }

        private async Task UpdateRuleAsync()
        {
            try
            {
                if (await EditingRuleValidations.ValidateAll() == false)
                {
                    return;
                }

                await RulesAppService.UpdateAsync(EditingRuleId, EditingRule);
                await GetRulesAsync();
                await EditRuleModal.Hide();                
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

        protected virtual async Task OnRuleCodeChangedAsync(string? ruleCode)
        {
            Filter.RuleCode = ruleCode;
            await SearchAsync();
        }
        protected virtual async Task OnSuccessEventChangedAsync(string? successEvent)
        {
            Filter.SuccessEvent = successEvent;
            await SearchAsync();
        }
        protected virtual async Task OnErrorMessageChangedAsync(string? errorMessage)
        {
            Filter.ErrorMessage = errorMessage;
            await SearchAsync();
        }
        protected virtual async Task OnErrorTypeChangedAsync(ErrorType? errorType)
        {
            Filter.ErrorType = errorType;
            await SearchAsync();
        }
        protected virtual async Task OnRuleExpressionTypeChangedAsync(RuleExpressionType? ruleExpressionType)
        {
            Filter.RuleExpressionType = ruleExpressionType;
            await SearchAsync();
        }
        protected virtual async Task OnExpressionChangedAsync(string? expression)
        {
            Filter.Expression = expression;
            await SearchAsync();
        }
        protected virtual async Task OnDescriptionChangedAsync(string? description)
        {
            Filter.Description = description;
            await SearchAsync();
        }
        

    }
}
