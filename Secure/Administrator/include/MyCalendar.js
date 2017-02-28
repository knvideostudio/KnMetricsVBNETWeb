// JScript File
// Calendar function


var popUp; 
var popUpPractice;
var popUpAddDelete;

function OpenCalendar(idname)
{
	
	//var w = screen.width - 500;
	//var h = screen.height - 250;
	
	popUp = window.open('include/SpecialCalendar.aspx?formname=' + document.forms[0].name + 
		'&id=' + idname + '&selected=' + document.forms[0].elements[idname].value, 
		'popupcal', 
		'width=270,height=300,left=200,top=200');
	popUp.focus();
}

function SetDate(formName, id, newDate)
{
	eval('var theform = document.' + formName + ';');
	popUp.close();
	theform.elements[id].value = newDate;
	// return focus to selected field
	theform.elements[id].focus();
}		

function CloseWindow()
{
	self.close();
}

function AddDeletePractice(id, act)
{
	popUpAddDelete = window.open('AddDeletePractice.aspx?id=' + id + '&act=' + act, 
		'AddDeletePractices', 
		'width=1000,height=600,status=yes,toolbar=no,menubar=no,location=no,resizable=yes,scrollbars=yes,left=20,top=20');
	popUpAddDelete.focus();
}

function OpenEditPractice(id, dropId)
{
	popUpPractice = window.open('RmdDropPractice.aspx?id=' + id + '&dropId=' + dropId, 
		'EditPractices', 
		'width=1000,height=600,status=yes,toolbar=no,menubar=no,location=no,resizable=yes,scrollbars=yes,left=20,top=20');
	popUpPractice.focus();
}

function ClosePractice()
{
    if (popUpPractice.closed == false)
    {
       popUpPractice.close();
    }
}

function CloseCalendar()
{
    if (popUp.closed == false)
    {
       popUp.close();
    }
}

//function DoPostBack();
//{
//    __doPostBack('','');
//}