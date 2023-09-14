$(function () {
    var l = abp.localization.getResource("RulesEngine");
	
	var rulesMemberService = window.jS.abp.rulesEngine.rulesMembers.rulesMember;
	
        var lastNpIdId = '';
        var lastNpDisplayNameId = '';

        var _lookupModal = new abp.ModalManager({
            viewUrl: abp.appPath + "Shared/LookupModal",
            scriptUrl: "/Pages/Shared/lookupModal.js",
            modalClass: "navigationPropertyLookup"
        });

    
        $('.lookupCleanButton').on('click', '', function () {
            $(this).parent().find('input').val('');
        });

        _lookupModal.onClose(function () {
            var modal = $(_lookupModal.getModal());
            $('#' + lastNpIdId).val(modal.find('#CurrentLookupId').val());
            $('#' + lastNpDisplayNameId).val(modal.find('#CurrentLookupDisplayName').val());
        });
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "RulesEngine/RulesMembers/CreateModal",
        scriptUrl: "/Pages/RulesEngine/RulesMembers/createModal.js",
        modalClass: "rulesMemberCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "RulesEngine/RulesMembers/EditModal",
        scriptUrl: "/Pages/RulesEngine/RulesMembers/editModal.js",
        modalClass: "rulesMemberEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            sequenceMin: $("#SequenceFilterMin").val(),
			sequenceMax: $("#SequenceFilterMax").val(),
			description: $("#DescriptionFilter").val(),
			rulesGroupId: $("#RulesGroupId").val(),
            ruleId: $("#RuleIdFilter").val()
        };
    };
    


    var dataTable = $("#RulesMembersTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(rulesMemberService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('RulesEngine.RulesMembers.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.rulesMember.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('RulesEngine.RulesMembers.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    rulesMemberService.delete(data.record.rulesMember.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reloadEx();;
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "rulesMember.sequence" },
            {
                data: "rule.ruleCode",
                defaultContent : ""
            },
			{ data: "rulesMember.description" }
            // {
            //     data: "rulesGroup.groupName",
            //     defaultContent : ""
            // },
            
        ]
    }));
    
    

    createModal.onResult(function () {
        dataTable.ajax.reloadEx();;
    });

    editModal.onResult(function () {
        dataTable.ajax.reloadEx();;
    });

    $("#NewRulesMemberButton").click(function (e) {
        e.preventDefault();
        createModal.open({RulesGroupId: $("#RulesGroupId").val()});
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reloadEx();;
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        rulesMemberService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/rules-engine/rules-members/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText },
                            { name: 'sequenceMin', value: input.sequenceMin },
                            { name: 'sequenceMax', value: input.sequenceMax }, 
                            { name: 'description', value: input.description }, 
                            { name: 'rulesGroupId', value: input.rulesGroupId }
, 
                            { name: 'ruleId', value: input.ruleId }
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
