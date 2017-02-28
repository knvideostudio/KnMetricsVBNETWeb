// JScript File

var popUp; 

function OpenCalendar(idname)
{
	
	var w = screen.width - 500;
	var h = screen.height - 250;
	
	popUp = window.open('include/SpecialCalendar.aspx?formname=' + document.forms[0].name + 
		'&id=' + idname + '&selected=' + document.forms[0].elements[idname].value, 
		'popupcal', 
		'width=270,height=300,left=200,top=200');
	popUp.focus();
}


function displaytime()
{
    var d = new Date();
    var h = d.getHours();
    var m = d.getMinutes();
    
    var ampm = (h >= 12) ? "PM":"AM";
    if (h > 12) h -= 12;
    if (h == 0) h = 12;
    if (m < 10) m = "0" + m;
    
    var t = h + ':' + m + ' ' + ampm;
    
    defaultStatus = t;
    
    setTimeout("displaytime()", 60000) // this one minute
      
}

function getbrowser()
{
    var browser = "BROWSER INFORMATION:\n";

    for(var propname in navigator)
    {
        browser += propname + ": " + navigator[propname] + "\n"
    }
    
    alert(browser);
    
}

// determines browser version 

function getbrowserVersion()
{
    // create new object
    var browser = new Object();
    
    
}
