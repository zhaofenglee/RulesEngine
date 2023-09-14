$(function () {
    var l = abp.localization.getResource("RulesEngine");
	
	var rulesGroupService = window.jS.abp.rulesEngine.rulesGroups.rulesGroup;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "RulesEngine/RulesGroups/CreateModal",
        scriptUrl: "/Pages/RulesEngine/RulesGroups/createModal.js",
        modalClass: "rulesGroupCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "RulesEngine/RulesGroups/EditModal",
        scriptUrl: "/Pages/RulesEngine/RulesGroups/editModal.js",
        modalClass: "rulesGroupEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            groupName: $("#GroupNameFilter").val(),
			operatorType: $("#OperatorTypeFilter").val(),
			description: $("#DescriptionFilter").val()
        };
    };
    


    var dataTable = $("#RulesGroupsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(rulesGroupService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("EditDetails"),
                                visible: abp.auth.isGranted('RulesEngine.RulesMembers'),
                                action: function(data) {
                                    window.location = "/RulesEngine/RulesGroups/" + data.record.id;
                                }
                            },
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('RulesEngine.RulesGroups.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('RulesEngine.RulesGroups.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    rulesGroupService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reloadEx();;
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "groupName" },
            {
                data: "operatorType",
                render: function (operatorType) {
                    if (operatorType === undefined ||
                        operatorType === null) {
                        return "";
                    }

                    var localizationKey = "Enum:OperatorType." + operatorType;
                    var localized = l(localizationKey);

                    if (localized === localizationKey) {
                        abp.log.warn("No localization found for " + localizationKey);
                        return "";
                    }

                    return localized;
                }
            },
			{ data: "description" }
        ]
    }));
    
  

    createModal.onResult(function () {
        dataTable.ajax.reloadEx();;
    });

    editModal.onResult(function () {
        dataTable.ajax.reloadEx();;
    });

    $("#NewRulesGroupButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reloadEx();;
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        rulesGroupService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/rules-engine/rules-groups/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'groupName', value: input.groupName }, 
                            { name: 'operatorType', value: input.operatorType }, 
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
