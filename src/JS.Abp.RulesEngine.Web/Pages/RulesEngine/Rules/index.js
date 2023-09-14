$(function () {
    var l = abp.localization.getResource("RulesEngine");
	
	var ruleService = window.jS.abp.rulesEngine.rules.rule;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "RulesEngine/Rules/CreateModal",
        scriptUrl: "/Pages/RulesEngine/Rules/createModal.js",
        modalClass: "ruleCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "RulesEngine/Rules/EditModal",
        scriptUrl: "/Pages/RulesEngine/Rules/editModal.js",
        modalClass: "ruleEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            ruleCode: $("#RuleCodeFilter").val(),
			successEvent: $("#SuccessEventFilter").val(),
			errorMessage: $("#ErrorMessageFilter").val(),
			errorType: $("#ErrorTypeFilter").val(),
			ruleExpressionType: $("#RuleExpressionTypeFilter").val(),
			expression: $("#ExpressionFilter").val(),
			description: $("#DescriptionFilter").val()
        };
    };
    
    

    var dataTable = $("#RulesTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(ruleService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('RulesEngine.Rules.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('RulesEngine.Rules.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    ruleService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reloadEx();;
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "ruleCode" },
			{ data: "successEvent" },
			{ data: "errorMessage" },
            {
                data: "errorType",
                render: function (errorType) {
                    if (errorType === undefined ||
                        errorType === null) {
                        return "";
                    }

                    var localizationKey = "Enum:ErrorType." + errorType;
                    var localized = l(localizationKey);

                    if (localized === localizationKey) {
                        abp.log.warn("No localization found for " + localizationKey);
                        return "";
                    }

                    return localized;
                }
            },
            {
                data: "ruleExpressionType",
                render: function (ruleExpressionType) {
                    if (ruleExpressionType === undefined ||
                        ruleExpressionType === null) {
                        return "";
                    }

                    var localizationKey = "Enum:RuleExpressionType." + ruleExpressionType;
                    var localized = l(localizationKey);

                    if (localized === localizationKey) {
                        abp.log.warn("No localization found for " + localizationKey);
                        return "";
                    }

                    return localized;
                }
            },
			{ data: "expression" },
			{ data: "description" }
        ]
    }));
    
    

    createModal.onResult(function () {
        dataTable.ajax.reloadEx();;
    });

    editModal.onResult(function () {
        dataTable.ajax.reloadEx();;
    });

    $("#NewRuleButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reloadEx();;
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        ruleService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/rules-engine/rules/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'ruleCode', value: input.ruleCode }, 
                            { name: 'successEvent', value: input.successEvent }, 
                            { name: 'errorMessage', value: input.errorMessage }, 
                            { name: 'errorType', value: input.errorType }, 
                            { name: 'ruleExpressionType', value: input.ruleExpressionType }, 
                            { name: 'expression', value: input.expression }, 
                            { name: 'description', value: input.description }
                            ]);
                            
                    var downloadWindow = window.open(url, '_blank');
                    downloadWindow.focus();
            }
        )
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reloadEx();;
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reloadEx();;
    });
    
    
    
    
});
