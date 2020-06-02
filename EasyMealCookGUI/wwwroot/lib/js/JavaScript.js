$('#customFile').on('change', function () {
    var fileName = $(this).val();
    var filenameShort = fileName.replace(/^.*\\/, "");
    $(this).next('.custom-file-label').html(filenameShort);
})
