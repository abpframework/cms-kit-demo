abp.modals.ImageManagementCreate = function () {

    function initModal(modalManager, args) {
        var fileUploadUri = "/api/cms-kit-admin/media/page";

        var fileInput = document.getElementById("ImageFile");
        var file;

        fileInput.addEventListener('change', function () {
            abp.ui.block();

            file = fileInput.files[0];

            if (file === undefined) {
                $("#ImageFile").val('');
                return;
            }

            var permittedExtensions = ["jpg", "jpeg", "png"]
            var fileExtension = $(this).val().split('.').pop();
            if (permittedExtensions.indexOf(fileExtension) === -1) {
                abp.message.error(l('ThisExtensionIsNotAllowed'))
                    .then(() => {
                        $("#ImageFile").val('');
                        file = null;
                    });
            }
            else if (file.size > 1024 * 1024) {
                abp.message.error(l('TheFileIsTooBig'))
                    .then(() => {
                        $('#ImageFile').val('');
                        file = null;
                    });
                return;
            }

            uploadImage();

            abp.ui.unblock();
        });

        function uploadImage() {
            var formData = new FormData();
            formData.append("name", file.name);
            formData.append("file", file);

            $.ajax(
                {
                    url: fileUploadUri,
                    data: formData,
                    processData: false,
                    contentType: false,
                    type: "POST",
                    success: function (data) {
                        $("#Image_CoverImageMediaId").val(data.id);
                    },
                    error: function (err) {
                        abp.message.error(err);
                    }
                }
            );
        }
    };

    return {
        initModal: initModal
    };
};
