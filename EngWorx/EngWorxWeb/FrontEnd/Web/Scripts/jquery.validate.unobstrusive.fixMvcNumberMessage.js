(function ($) {
    $.each($.validator.unobtrusive.adapters, function () {
        if (this.name === "number") {
            var baseAdapt = this.adapt;
            this.adapt = function (options) {
                var fieldName = new RegExp("Il Campo (.+) deve essere un numerico").exec(options.message)[1];
                options.message = $.validator.format($.validator.messages.number, fieldName);
                baseAdapt(options);
            };
        }
    });
} (jQuery));