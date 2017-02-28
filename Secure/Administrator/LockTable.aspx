<%@ Page Theme="LockColumn" Language="VB" AutoEventWireup="false" CodeFile="LockTable.aspx.vb" Inherits="Secure_Administrator_LockTable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
<script type="text/javascript">
function lockCol(tblID) {

	var table = document.getElementById(tblID);
	var button = document.getElementById('toggle');
	var cTR = table.getElementsByTagName('TR');  //collection of rows

	if (table.rows[0].cells[0].className == '') {
		for (i = 0; i < cTR.length; i++)
			{
			var tr = cTR.item(i);
			tr.cells[0].className = 'locked'
			tr.cells[1].className = 'locked'
			tr.cells[2].className = 'locked'
			}
		button.innerText = "Unlock First Column";
		}
		else {
		for (i = 0; i < cTR.length; i++)
			{
			var tr = cTR.item(i);
			tr.cells[0].className = ''
			tr.cells[1].className = ''
			tr.cells[2].className = ''
			}
		button.innerText = "Lock First Column";
		}
}
</script>


</head>
<body>

<h2 style="color:navy">Lock or Freeze Table Columns plus Non-Scroll Headers  <br><span style="font-size:75%;">(Internet Explorer CSS solution)</span></h2>

<div class="infobox">
<p>In Internet Explorer 5.0+ (Windows), this table has a <b>non-scrolling header</b> with the option to create a <b>locked column</b> for horizontal scrolling.</p>
<p>This solution is very lightweight and easy to maintain. No scripts are used to produce the effects, except one-line IE expressions applied through the style sheet.</p>
<p>The column locking is <b>accomplished entirely by means of styling</b> which can be applied by a static assignment of a CSS class to the cells of the locked column, or as in this example, by dynamically toggling the class upon user action. Waiting for explicit user action to lock the column has the effect of speeding the display of large tables. </p>

<p><a href="http://web.tampabay.rr.com/bmerkey/">Brett Merkey</a></p>

<p><a href="freeze-columns.html">Some aesthetic tweaks</a> and a 

<a href="move-lock-col.html">Drag and drop variation</a></p>

<p><a href="http://web.tampabay.rr.com/bmerkey/examples/css-buttons2.html">Other experiments</a></p>
</div>

<button onClick="lockCol('tbl')"  id="toggle">Lock First Column</button>
<button onClick="document.location='locked-column.css.html' ">View Styles</button>

<div id="tbl-container">

<table id="tbl">
<thead>
<tr>
<th>Name</th>
<th>Major</th>
<th>Sex</th>
<th>English</th>
<th>Japanese</th>
<th>Calculus</th>
<th>Geometry</th>
</tr>
</thead>

<tbody>

  <tr>
    <td>Student01</td>
    <td>Languages</td>
    <td>male</td>
    <td>80</td>
    <td>70</td>
    <td>75</td>
    <td>80</td>

  </tr>
  <tr>
    <td>Student02</td>
    <td>Mathematics</td>
    <td>male</td>
    <td>90</td>
    <td>88</td>
    <td>100</td>
    <td>90</td>

  </tr>
  <tr>
    <td>Student03</td>
    <td>Languages</td>
    <td>female</td>
    <td>85</td>
    <td>95</td>
    <td>80</td>
    <td>85</td>

  </tr>
  <tr>
    <td>Student04</td>
    <td>Languages</td>
    <td>male</td>
    <td>60</td>
    <td>55</td>
    <td>100</td>
    <td>100</td>

  </tr>
  <tr>
    <td>Student05</td>
    <td>Languages</td>
    <td>female</td>
    <td>68</td>
    <td>80</td>
    <td>95</td>
    <td>80</td>

  </tr>
  <tr>
    <td>Student06</td>
    <td>Mathematics</td>
    <td>male</td>
    <td>100</td>
    <td>99</td>
    <td>100</td>
    <td>90</td>

  </tr>
  <tr>
    <td>Student07</td>
    <td>Mathematics</td>
    <td>male</td>
    <td>85</td>
    <td>68</td>
    <td>90</td>
    <td>90</td>

  </tr>
  <tr>
    <td>Student08</td>
    <td>Languages</td>
    <td>male</td>
    <td>100</td>
    <td>90</td>
    <td>90</td>
    <td>85</td>

  </tr>
  <tr>
    <td>Student09</td>
    <td>Mathematics</td>
    <td>male</td>
    <td>80</td>
    <td>50</td>
    <td>65</td>
    <td>75</td>

  </tr>
  <tr>
    <td>Student10</td>
    <td>Languages</td>
    <td>male</td>
    <td>85</td>
    <td>100</td>
    <td>100</td>
    <td>90</td>

  </tr>
  <tr>
    <td>Student11</td>
    <td>Languages</td>
    <td>male</td>
    <td>86</td>
    <td>85</td>
    <td>100</td>
    <td>100</td>

  </tr>
  <tr>
    <td>Student12</td>
    <td>Mathematics</td>
    <td>female</td>
    <td>100</td>
    <td>75</td>
    <td>70</td>
    <td>85</td>

  </tr>
  <tr>
    <td>Student13</td>
    <td>Languages</td>
    <td>female</td>
    <td>100</td>
    <td>80</td>
    <td>100</td>
    <td>90</td>

  </tr>
  <tr>
    <td>Student14</td>
    <td>Languages</td>
    <td>female</td>
    <td>50</td>
    <td>45</td>
    <td>55</td>
    <td>90</td>

  </tr>
  <tr>
    <td>Student15</td>
    <td>Languages</td>
    <td>male</td>
    <td>95</td>
    <td>35</td>
    <td>100</td>
    <td>90</td>

  </tr>
  <tr>
    <td>Student16</td>
    <td>Languages</td>
    <td>female</td>
    <td>100</td>
    <td>50</td>
    <td>30</td>
    <td>70</td>

  </tr>
  <tr>
    <td>Student17</td>
    <td>Languages</td>
    <td>female</td>
    <td>80</td>
    <td>100</td>
    <td>55</td>
    <td>65</td>

  </tr>
  <tr>
    <td>Student18</td>
    <td>Mathematics</td>
    <td>male</td>
    <td>30</td>
    <td>49</td>
    <td>55</td>
    <td>75</td>

  </tr>
  <tr>
    <td>Student19</td>
    <td>Languages</td>
    <td>male</td>
    <td>68</td>
    <td>90</td>
    <td>88</td>
    <td>70</td>

  </tr>
  <tr>
    <td>Student20</td>
    <td>Mathematics</td>
    <td>male</td>
    <td>40</td>
    <td>45</td>
    <td>40</td>
    <td>80</td>

  </tr>
  <tr>
    <td>Student21</td>
    <td>Languages</td>
    <td>male</td>
    <td>50</td>
    <td>45</td>
    <td>100</td>
    <td>100</td>

  </tr>
  <tr>
    <td>Student22</td>
    <td>Mathematics</td>
    <td>male</td>
    <td>100</td>
    <td>99</td>
    <td>100</td>
    <td>90</td>

  </tr>
  <tr>
    <td>Student23</td>
    <td>Languages</td>
    <td>female</td>
    <td>85</td>
    <td>80</td>
    <td>80</td>
    <td>80</td>

  </tr>
  </tbody>
</table>


</div> <!-- end tbl-container -->

<blockquote>
&#8220;I just wanted to offer a big thank you for your column locking solution. It looks like it's on its way to 'saving our project,' at least in my usability-conscious eyes.&#8221;<br>
<span class="sig">Jack Bellis <a href="http://usabilityinstitute.com/articles/moreTech.htm">Usability Institute</a></span>
</blockquote>

<blockquote>
&#8220;It's true! Gee wizz, every time I think I know it all, bamm! Right between
the eyes.&#8221;<br>
<span class="sig">Big John <a href="http://www.positioniseverything.net/">Position is Everything</a></span>
</blockquote>

<blockquote style="width: 98%">
Note: For very large tables, avoid performance problems by applying Internet Explorer's client-side data binding feature to divide data tables into "pages" with a set number of visible rows.   This means that scripts and styles have only to act on the visible portion of the table, dramatically improving performance. Using data binding, tables with 1200 rows and 20 columns were tested with absolutely no performance issues.
</blockquote>



</body>
</html>

