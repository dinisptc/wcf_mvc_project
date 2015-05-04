
$(document).ready(function () {




    document.getElementById('textEditor').contentWindow.document.designMode = "on";
    document.getElementById('textEditor').contentWindow.document.close();

//    editon();

    $("#bold").click(function () {
        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected");
        } else {
            $(this).addClass("selected");
        }
        boldIt();
    });
    $("#italic").click(function () {

        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected");
        } else {
            $(this).addClass("selected");
        }
        ItalicIt();
    });

    $("#imageit").click(function () {


        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected");
        } else {
            $(this).addClass("selected");
        }
        ImageIt();
    });

    $("#redit").click(function () {
        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected");

        } else {
            $(this).addClass("selected");
        }
        RedIt();
    });

    $("#blueit").click(function () {
        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected");

        } else {
            $(this).addClass("selected");
        }
        BlueIt();
    });

    $("#blackit").click(function () {
        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected");

        } else {
            $(this).addClass("selected");
        }
        BlackIt();
    });

    $("#addlink").click(function () {
        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected");

        } else {
            $(this).addClass("selected");
        }
        AddLink();
    });
    $("#theimgs").click(function () {
        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected");

        } else {
            $(this).addClass("selected");
        }
        theimage();
    });

    $("#img").click(function () {

        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected");

        } else {
            $(this).addClass("selected");
        }
        imgt();
    });



    $("#thehtmlsel").click(function () {

        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected");

        } else {
            $(this).addClass("selected");
        }
        H1It();
    });

    $("#hn").click(function () {

        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected");

        } else {
            $(this).addClass("selected");
        }
        HNIt();
    });


    $("#h2").click(function () {

        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected");

        } else {
            $(this).addClass("selected");
        }
        H2It();
    });


    $("#insertOrderedList").click(function () {

        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected");

        } else {
            $(this).addClass("selected");
        }
        insertOrderedListit();
    });

    $("#underline").click(function () {

        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected");

        } else {
            $(this).addClass("selected");
        }
        underlineit();
    });


    $("#source").click(function () {

        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected");

        } else {
            $(this).addClass("selected");
        }
        source();
    });


    $("#cleansource").click(function () {

        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected");

        } else {
            $(this).addClass("selected");
        }
        clearsource();
    });

    $("#save").click(function () {


        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected");

        } else {
            $(this).addClass("selected");
        }

        on();

    });


    $("#hidden_field_id").click(function () {


        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected");

        } else {
            $(this).addClass("selected");
        }

        on();
    });
});



function on() {
 
    var hidden = document.getElementById("hidden_field_id").contentWindow;
    var edit = document.getElementById("textEditor").contentWindow;
    edit.focus();

    var ifi = document.getElementById('textEditor');
    var hid = document.getElementById('hidden_field_id');

    if (ifi.contentWindow.document.body.innerHTML == "") {
        alert("Falta a descrição");

    } else {
        //alert(ifi.contentWindow.document.body.innerHTML);


        $("#hidden_field_id").val(ifi.contentWindow.document.body.innerHTML);
    }
    edit.focus();
}

function imgt() {

    var edit = document.getElementById("textEditor").contentWindow;
    edit.focus();

    edit.document.execCommand('foreColor', false, "#000000");

    edit.focus();
}
//You can go on adding different functionalities  
function boldIt() {
    var edit = document.getElementById("textEditor").contentWindow;
    edit.focus();
    edit.document.execCommand("bold", false, "");
    edit.focus();
}

function ItalicIt() {
    var edit = document.getElementById("textEditor").contentWindow;
    edit.focus();
    edit.document.execCommand("italic", false, "");
    edit.focus();
}


function ImageIt() {
    imagePath = prompt('Enter Image URL:', 'http://');	
    var edit = document.getElementById("textEditor").contentWindow;
    edit.focus();

     edit.document.execCommand('InsertImage', false, imagePath);

    edit.focus();
}

function RedIt() {
    //imagePath = prompt('Enter Image URL:', 'http://');
    var edit = document.getElementById("textEditor").contentWindow;
    edit.focus();

    edit.document.execCommand('foreColor', false, "#ff0000");

    edit.focus();
}
function BlueIt() {
    //imagePath = prompt('Enter Image URL:', 'http://');
    var edit = document.getElementById("textEditor").contentWindow;
    edit.focus();

    edit.document.execCommand('foreColor', false, "#0000ff");

    edit.focus();
}
function BlackIt() {

    var edit = document.getElementById("textEditor").contentWindow;
    edit.focus();

    edit.document.execCommand('foreColor', false, "#000000");

    edit.focus();
}

function H1It() {

    var edit = document.getElementById("textEditor").contentWindow;
    edit.focus();
    edit.document.execCommand("fontsize", "", "10");
    edit.focus();
}

function HNIt() {

    var edit = document.getElementById("textEditor").contentWindow;
    edit.focus();
    edit.document.execCommand("FontSize", false,"3");
    edit.focus();
}

function H2It() {

    var edit = document.getElementById("textEditor").contentWindow;
    edit.focus();
    edit.document.execCommand("FontSize", false, "5");
    edit.focus();
}

function insertOrderedListit() {

    var edit = document.getElementById("textEditor").contentWindow;
    edit.focus();
    edit.document.execCommand("insertOrderedList", false, "");
    edit.focus();
}


function underlineit() {

    var edit = document.getElementById("textEditor").contentWindow;
    edit.focus();
    edit.document.execCommand("underline", false, "");
    edit.focus();
}


function source() {

    var edit = document.getElementById("textEditor").contentWindow;
    edit.focus();
//    edit.document.execCommand("code", false, "");
    edit.document.execCommand("backcolor", false, "gray");
    edit.document.execCommand("ForeColor", false, "#FFFFFF");
    edit.focus();
}


function clearsource() {

    var edit = document.getElementById("textEditor").contentWindow;
    edit.focus();
    edit.document.execCommand("backcolor", false, "white");
    edit.document.execCommand("ForeColor", false, "#000000");
    edit.focus();
}
/*
function source() {
    //identify selected text
    var edit = document.getElementById("textEditor").contentWindow;
    var sText = edit.document.selection.createRange();
    if (sText.text != "") {
        //create link
        //edit.document.execCommand("CreateLink");
        edit.document.execCommand("backcolor", false, "gray");
        edit.document.execCommand("ForeColor", false, "#FFFFFF");
        if (sText.parentElement().backcolor == "gray") {
            sText.execCommand("ForeColor", false, "#FF0033");
        }
    }
    else {
        alert("Please select some text!");
    }
}*/

function AddLink() {
    //identify selected text
    var edit = document.getElementById("textEditor").contentWindow;
    var sText = edit.document.selection.createRange();
    if (sText.text != "") {
        //create link
        edit.document.execCommand("CreateLink");
        //change the color to indicate success
        if (sText.parentElement().tagName == "A") {
            sText.execCommand("ForeColor", false, "#FF0033");
        }
    }
    else {
        alert("Please select some text!");
    }
}

function theimage() {
    var edit = document.getElementById("textEditor").contentWindow;
    var style = '" style="float:left';
    var imgURL = document.getElementById('imageurl').value + style;
    edit.execCommand("insertimage", imgURL);
}
/*
function CallWindowOpener(returnValue) {
    if (typeof ReturnValueFromPopup == 'function') {
        ReturnValueFromPopup(returnValue);

    }
}

function saveImageAs(imgOrURL) {
    if (typeof imgOrURL == 'object')
        imgOrURL = imgOrURL.src;
    window.win = open(imgOrURL);
    setTimeout('win.document.execCommand("SaveAs")', 500);
} 
*/


function iImage() {
    var imgSrc = prompt('Enter image location', '');
    if (imgSrc != null) {
        richTextField.document.execCommand('insertimage', false, imgSrc);
    }
}


function saveImageAs(imgOrURL) {
    if (typeof imgOrURL == 'object')
        imgOrURL = imgOrURL.src;
    window.win = open(imgOrURL);
    setTimeout('win.document.execCommand("SaveAs")', 500);
}

$(function () {


    $("#dialog").dialog({
        bgiframe: true,
        height: 140,
        modal: true,
        autoOpen: false,
        resizable: false
    })
});

function thehtml() {
    var htm = "<p>The following is a list:";
    htm += "<ol>";
    htm += "<li>This is the first item";
    htm += "<li>This is the second item";
    htm += "<li>This is the third item";
    htm += '</ol>';
    document.execCommand('insertHTML', false, htm);

}

/*
function thehtmlselect() {
    //identify selected text
    var edit = document.getElementById("textEditor").contentWindow;
    var sText = edit.document.selection.createRange();
    if (sText.text != "") {
        //create link
       // edit.document.execCommand("CreateLink");
        var htm = "<p>The following is a list:";
        htm += "<ol>";
        htm += "<li>This is the first item";
        htm += "<li>This is the second item";
        htm += "<li>This is the third item";
        htm += '</ol>';
        edit.document.execCommand('insertHTML', false, htm);
        sText.execCommand('insertHTML', false, htm);
        //change the color to indicate success
        if (sText.parentElement().tagName == "A") {
            sText.execCommand("ForeColor", false, "#FF0033");
        }
    }
    else {
        alert("Please select some text!");
    }
}*/




function thehtmlselect() {




    //identify selected text
    var edit = document.getElementById("textEditor").contentWindow;

//    edit.formatblock = {
//        'Normal': 'nosidebar',
//        'Heading': 'h2',
//        'Subheading': 'h3',
//        'Pre-formatted': 'pre',
//        'Sidebar': 'sidebar'
//    };
//    var sText = edit.document.selection.createRange();
//    if (sText.text != "") {

//        var htm = "<p>The following is a list:";
//        htm += "<ol>";
//        htm += "<li>This is the first item";
//        htm += "<li>This is the second item";
//        htm += "<li>This is the third item";
//        htm += '</ol>';
//        edit.document.execCommand('insertHTML', false, htm);
//        sText.execCommand('insertHTML', false, htm);

//    var edit = document.getElementById("textEditor").contentWindow;
    edit.focus();
    edit.document.execCommand('formatblock', false,'h1');
    edit.focus();
//        edit.document.execCommand('formatBlock', false, 'H1')
//        sText.execCommand('formatBlock', false, 'H1')
        //create link
       // edit.document.execCommand("CreateLink");
        //change the color to indicate success
        //if (sText.parentElement().tagName == "A") {
        //  sText.execCommand("ForeColor", false, "#FF0033");
        //}
//    }
//    else {
//        alert("Please select some text!");
//    }
}