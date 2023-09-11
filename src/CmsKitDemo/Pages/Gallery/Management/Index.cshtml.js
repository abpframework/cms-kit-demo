$(function () {
    var l = abp.localization.getResource('CmsKitDemo');
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Gallery/Management/CreateModal',
        modalClass: "ImageManagementCreate"
    });

    var dataTable = $("#ImagesTable").DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(cmsKitDemo.services.imageGallery.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('CmsKitDemo.GalleryImage.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('CmsKitDemo.GalleryImage.Delete'),
                                    confirmMessage: function (data) {
                                        return l('ImageDeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        cmsKitDemo.services.imageGallery
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(l('SuccessfullyDeleted'));
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l("Description"),
                    data: "description"
                },
                {
                    title: l("CreationTime"),
                    data: "creationTime",
                    dataFormat: "datetime"
                }
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewImageButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});