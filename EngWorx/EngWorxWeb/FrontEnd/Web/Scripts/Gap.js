(function ($) {
    $.fn.invisible = function () {
        return this.each(function () {
            $(this).css("visibility", "hidden");
        });
    };
    $.fn.visible = function () {
        return this.each(function () {
            $(this).css("visibility", "visible");
        });
    };
} (jQuery));
function setWaitingOnClick(buttonClicked, itemToShow, itemToHide) {
    if (typeof itemToShow != "undefined") {
        itemToShow.invisible();
    }
    buttonClicked.click(function () {
        //alert(buttonClicked);
        //alert(itemToShow);
        //alert(itemToHide);
        var form = $(this).closest("form");
        resetValidationErrors(form);
        resetCustomErrorMessage();
        if ((!form.valid) || (form.valid && form.valid())) {
            buttonClicked.attr("disabled", true);
            if (typeof itemToShow != "undefined") {
                itemToShow.visible();
            }
            if (typeof itemToHide != "undefined") {
                itemToHide.invisible();
            }
            if ($.browser.msie) {
                //serve per IE
                this.form.submit();
            }
            return true;
        } else {
            return false;
        }
    });
}
function setWaitingOnClick2(buttonClicked) {
    $(buttonClicked).click(function () {
        //alert("qui");
        //return true;
    });
}
function resetValidationErrors($form) {
    //if "validate" function exists
    if ($form.validate) {
        //reset jQuery Validate's internals
        $form.validate().resetForm();
    }

    //reset unobtrusive validation summary, if it exists
    $form.find("[data-valmsg-summary=true]")
            .removeClass("validation-summary-errors")
            .addClass("validation-summary-valid")
            .find("ul").empty();

    //reset unobtrusive field level, if it exists
    $form.find("[data-valmsg-replace]")
            .removeClass("field-validation-error")
            .addClass("field-validation-valid");
}
function resetCustomErrorMessage() {
    $("#lblErrorMessage").empty();
}
function isValidLogin() {
    var result = true;
    if (document.getElementById("company").value == "" || document.getElementById("username").value == "" || document.getElementById("password").value == "") {
        result = false;
    }
    return result;
}
function setFocus(nameTxt) {
    var control = document.getElementById(nameTxt);
    if (control != null) {
        control.focus();
    }
}
function loadPopupModal(action) {
    var windowsWidth = 150;
    var windowsHeigth = 220;
    var xPos = (screen.width - windowsWidth) / 2;

    $('#modalPopupDetail').html('');
    $("#modalPopupDetail").load(action);
    $("#modalPopupDetail").dialog({
        modal: true,
        width: windowsWidth,
        height: windowsHeigth,
        position: [xPos, -150],
        title: 'RIR',
        buttons: {
            Ok: function () {
                $(this).dialog("close");
            }
        }
    })
}
function loadPopupModalDetails(action) {
    var windowsWidth= 650;
    var windowsHeigth=420;
    var xPos = (screen.width -windowsWidth)/2 ;
    scroll(0,0);
    $('#modalPopupDetail').html('');
    $("#modalPopupDetail").load( action + (/\?/.test(action) ? "&" : "?") + "_=" + (new Date()).getTime() );
    $("#modalPopupDetail").dialog({                    
        modal: true,
        width: windowsWidth,
        height: windowsHeigth,
        position: [xPos,-150],
        title: 'Dettaglio RIR',
        buttons: {
            Chiudi: function () {
                $(this).dialog("close");
            }
        }
    });
}
function selectAllChk(sender, item) {
    var isChecked = $("#" + sender).attr('checked');
    $("input[name=" + item + "]").each(function () {
         $(this).attr('checked', isChecked);
    });
 }

 function updatePermissionButton(btn,imgadd,imgrem) {
     var img = $("#" + btn).attr("src");
     if (imgadd.replace("~", "") === img) {
         $("#" + btn).attr("src", imgrem);
         $("#" + btn).attr("title", "Nega Permesso");
         $("#" + btn).attr("alt", "Nega Permesso");
     }
     else {
         $("#" + btn).attr("src", imgadd);
         $("#" + btn).attr("title", "Concedi Permesso");
         $("#" + btn).attr("alt", "Concedi Permesso");
     }
 }

 function leftPad(num, totalChars, padWith) {
     num = num + "";
     padWith = (padWith) ? padWith : "0";
     if (num.length < totalChars) {
         while (num.length < totalChars) {
             num = padWith + num;
         }
     } else { }

     if (num.length > totalChars) { //if padWith was a multiple character string and num was overpadded
         num = num.substring((num.length - totalChars), totalChars);
     } else { }

     return num;
 }
 function autoselectTB(item) {
     item.select();
 }
 function IsNumeric(input) {
     return (input - 0) == input && input.length > 0;
 }
 /* Validazione data */
 function validateDateTB(field) {
     var checkstr = "0123456789";
     var DateField = field;
     var Datevalue = "";
     var DateTemp = "";
     var seperator = "/";
     var day;
     var month;
     var year;
     var leap = 0;
     var err = 0;
     var i;
     err = 0;
     DateValue = DateField.value;
     for (i = 0; i < DateValue.length; i++) {
         if (checkstr.indexOf(DateValue.substr(i, 1)) >= 0) {
             DateTemp = DateTemp + DateValue.substr(i, 1);
         }
     }
     DateValue = DateTemp;
     if (DateValue.length == 6) {
         DateValue = DateValue.substr(0, 4) + '20' + DateValue.substr(4, 2);
     }
     if (DateValue.length != 8) {
         err = 19;
     }
     year = DateValue.substr(4, 4);
     if (year == 0) {
         err = 20;
     }
     month = DateValue.substr(2, 2);
     if ((month < 1) || (month > 12)) {
         err = 21;
     }
     day = DateValue.substr(0, 2);
     if (day < 1) {
         err = 22;
     }
     if ((year % 4 == 0) || (year % 100 == 0) || (year % 400 == 0)) {
         leap = 1;
     }
     if ((month == 2) && (leap == 1) && (day > 29)) {
         err = 23;
     }
     if ((month == 2) && (leap != 1) && (day > 28)) {
         err = 24;
     }
     if ((day > 31) && ((month == "01") || (month == "03") || (month == "05") || (month == "07") || (month == "08") || (month == "10") || (month == "12"))) {
         err = 25;
     }
     if ((day > 30) && ((month == "04") || (month == "06") || (month == "09") || (month == "11"))) {
         err = 26;
     }
     if ((day == 0) && (month == 0) && (year == 00)) {
         err = 0; day = ""; month = ""; year = ""; seperator = "";
     }
     if (err == 0) {
         DateField.value = day + seperator + month + seperator + year;
         /* Se l'tem rispetta una determinata regual expression verra valorizzata la textbox to */
         checkidData(DateField);
     }
     else {
         alert("La data non e\' corretta!");
         setTimeout("document.getElementById('" + DateField.id + "').focus();", 1);
         setTimeout("document.getElementById('" + DateField.id + "').select();", 1);
         return false;
     }
     return true;
 }


 function filterDateValue(e, item) {
     var charCode = e.which || e.keyCode;
     if (charCode == 13) {
         if (validateDateTB_withoutMessage(item)) {
             return true;
         }
         else {
           var event = e || window.event;
           if (event.prevendDefault) {
               e.stopPropagation();
               e.preventDefault();
                event.preventDefault();
            } else {
                e.cancelBubble = false;
                e.returnValue = false;
            } 
            return false; 
         }
     }
     else {
         var v = item.value;
         if (document.selection) {
             var selectedText = document.selection.createRange().text || item.value.substr(item.selectionStart, (item.selectionEnd - item.selectionStart));
             // Validazione in base alla lunghezza 
             // Se nella textbox c'e' un numerico è la lunghezza del campo è ugale ad 8
             // e non è selezionato nessun carattere impedisce l'inserimento di altri caratteri
             if (IsNumeric(v) && v.length == 8) {
                 if (selectedText.length == 0) {
                     return false;
                 }
             }
             var reDate = /^\d{2}(\-|\/|\.)\d{2}\1\d{4}$/
             // Se nella textbox c'e' una data valida
             // e non è selezionato nessun carattere impedisce l'inserimento di altri caratteri
             if (reDate.test(v)) {
                 if (selectedText.length == 0) {
                     return false;
                 }
             }
             if (v.length == 10 && selectedText.length == 0)
                 return false;
             // Caratteri accettati 012345678 . / - altrimenti impedisce l'inserimento di altri caratteri
             if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46 && charCode != 45 && charCode != 47)
                 return false;
         }
         return true;
     }
 }
 function checkidData(DateField) {
     if (DateField.id.search(/\bFROM_DATE/) != -1) {
         filldata_to(DateField.id, DateField.value, "FROM_DATE", "TO_DATE");
     } else if (DateField.id.search(/\b_DA/) != -1) {
         filldata_to(DateField.id, DateField.value, "_DA", "_A");
     } else if (DateField.id.search(/\FROM/) != -1) {
         filldata_to(DateField.id, DateField.value , "FROM", "TO");
     }
     return 0;
 }
 function filldata_to(id, v, sFrom, sTo) {
     var datato = document.getElementById(id.replace(sFrom, sTo));
     if (datato) {
         if (datato.value.length == 0) {
             datato.value = v;
         }
     }
     return 0;
 }
 function validateDateTB_withoutMessage(field) {
     var checkstr = "0123456789";
     var DateField = field;
     var Datevalue = "";
     var DateTemp = "";
     var seperator = "/";
     var day;
     var month;
     var year;
     var leap = 0;
     var err = 0;
     var i;
     err = 0;
     DateValue = DateField.value;
     for (i = 0; i < DateValue.length; i++) {
         if (checkstr.indexOf(DateValue.substr(i, 1)) >= 0) {
             DateTemp = DateTemp + DateValue.substr(i, 1);
         }
     }
     DateValue = DateTemp;
     if (DateValue.length == 6) {
         DateValue = DateValue.substr(0, 4) + '20' + DateValue.substr(4, 2);
     }
     if (DateValue.length != 8) {
         err = 19;
     }
     year = DateValue.substr(4, 4);
     if (year == 0) {
         err = 20;
     }
     month = DateValue.substr(2, 2);
     if ((month < 1) || (month > 12)) {
         err = 21;
     }
     day = DateValue.substr(0, 2);
     if (day < 1) {
         err = 22;
     }
     if ((year % 4 == 0) || (year % 100 == 0) || (year % 400 == 0)) {
         leap = 1;
     }
     if ((month == 2) && (leap == 1) && (day > 29)) {
         err = 23;
     }
     if ((month == 2) && (leap != 1) && (day > 28)) {
         err = 24;
     }
     if ((day > 31) && ((month == "01") || (month == "03") || (month == "05") || (month == "07") || (month == "08") || (month == "10") || (month == "12"))) {
         err = 25;
     }
     if ((day > 30) && ((month == "04") || (month == "06") || (month == "09") || (month == "11"))) {
         err = 26;
     }
     if ((day == 0) && (month == 0) && (year == 00)) {
         err = 0; day = ""; month = ""; year = ""; seperator = "";
     }
     if (err == 0) {
         DateField.value = day + seperator + month + seperator + year;
     }
     else {
         setTimeout("document.getElementById('" + DateField.id + "').focus();", 1);
         setTimeout("document.getElementById('" + DateField.id + "').select();", 1);
         return false;
     }
 }

 function lpad(originalstr, length, strToPad) {
     while (originalstr.length < length)
         originalstr = strToPad + originalstr;
     return originalstr;
 }

 function rpad(originalstr, length, strToPad) {
     while (originalstr.length < length)
         originalstr = originalstr + strToPad;
     return originalstr;
 }
 

$(document).ready(function () {
     $(":input[case=upper]").each(function () {
         $(this).keyup(function () {
             this.value = this.value.toUpperCase();
         });
     });
     $(":input[case=lower]").each(function () {
         $(this).keyup(function () {
             this.value = this.value.toLowerCase();
         });
     });
 });

