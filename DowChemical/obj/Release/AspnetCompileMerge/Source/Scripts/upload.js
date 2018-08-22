(function (global, undefined) {
    function validationFailed(sender, eventArgs) {
        $telerik.$("#asyncUpload").html("Validation failed for '" + eventArgs.get_fileName() + "'.").fadeIn("slow");
    }

    global.validationFailed = validationFailed;

    function fileRemoved(sender, eventArgs) {
        $telerik.$("#asyncUpload").html('').fadeOut("slow");
    }

    global.fileRemoved = fileRemoved;
})(window);