<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucMenuBar.ascx.cs" Inherits="AdminSite.UserControls.ucMenuBar" %>
<style>
    ul {
      list-style: none;
      padding: 0;
      margin: 0;
      /*background: #1bc2a2;*/
    }

    ul li {
      display: block;
      position: relative;
      float: left;
      /*background: #1bc2a2;*/
    }

    /* This hides the dropdowns */


    li ul { 
        display: none; 
        background: #1B1B1B;
        margin-left: -10px;
    }

    ul li a {
      display: block;
      padding-left: 5px;
      padding-right: 5px;
      text-decoration: none;
      white-space: nowrap;
      color: #fff;
    }

    ul li a:hover { 
        /*background: #2c3e50;*/
    }

    /* Display the dropdown */


    li:hover > ul {
      display: block;
      position: absolute;
    }

    li:hover li { float: none; }

    li:hover a { 
        /*background: #1bc2a2;*/
    }

    li:hover li a:hover { 
        /*background: #2c3e50;*/
    }

    .main-navigation li ul li {
         border-top: 0;
         padding: 8px 10px 8px 10px;
         text-align: left;
         border-bottom: dotted #ccc 1px;
    }

    .main-navigation > li > ul {
         border-top: 0;
         padding-top: 13px;
         text-align: left;
    }

    /* Displays second level dropdowns to the right of the first level dropdown */


    ul ul ul {
      left: 100%;
      top: 0;
      background: #1B1B1B;
    }

    /* Simple clearfix */



    ul:before,
    ul:after {
      content: " "; /* 1 */
      display: table; /* 2 */
    }

    ul:after { clear: both; }
</style>
<ul id="ulMenuBar" runat="server" class="main-navigation">
                
</ul>