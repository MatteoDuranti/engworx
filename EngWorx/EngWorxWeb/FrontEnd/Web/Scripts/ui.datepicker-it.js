/* Italian initialisation for the jQuery UI date picker plugin. */
/* Written by Antonello Pasella (antonello.pasella@gmail.com). */
jQuery(function ($) {
    $.datepicker.regional['it'] = {
        closeText: 'Chiudi',
        prevText: '&#x3c;Prec',
        nextText: 'Succ&#x3e;',
        currentText: 'Oggi',
        monthNames: ['Gennaio', 'Febbraio', 'Marzo', 'Aprile', 'Maggio', 'Giugno',
			'Luglio', 'Agosto', 'Settembre', 'Ottobre', 'Novembre', 'Dicembre'],
        monthNamesShort: ['Gen', 'Feb', 'Mar', 'Apr', 'Mag', 'Giu',
			'Lug', 'Ago', 'Set', 'Ott', 'Nov', 'Dic'],
        dayNames: ['Domenica', 'Luned&#236', 'Marted&#236', 'Mercoled&#236', 'Gioved&#236', 'Venerd&#236', 'Sabato'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mer', 'Gio', 'Ven', 'Sab'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Me', 'Gi', 'Ve', 'Sa'],
        weekHeader: 'Sm',
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['it']);
}),

$.extend($.datepick, {
    /* Select a month only instead of a single day.
    Usage: onShow: $.datepick.monthOnly.
    @param  picker  (jQuery) the completed datepicker division
    @param  inst    (object) the current instance settings */
    monthOnly: function (picker, inst) {
        var target = $(this);
        var selectMonth = $('<div style="text-align: center;"><button type="button">Select</button></div>').
			insertAfter(picker.find('.datepick-month-row:last,.ui-datepicker-row-break:last')).
			children().click(function () {
			    var monthYear = picker.find('.datepick-month-year:first').val().split('/');
			    target.datepick('setDate', $.datepick.newDate(
					parseInt(monthYear[1], 10), parseInt(monthYear[0], 10), 1)).
					datepick('hide');
			});
        picker.find('.datepick-month-row table,.ui-datepicker-row-break table').remove();
    }
})
;