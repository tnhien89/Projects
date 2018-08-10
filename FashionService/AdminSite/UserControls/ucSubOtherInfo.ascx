<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucSubOtherInfo.ascx.cs" Inherits="AdminSite.UserControls.ucSubOtherInfo" %>

<div class="panel-heading panel-heading-custom">
    <label id="lbHeader" runat="server">Thông tin banner</label>
</div>
<div class="panel-body panel-body-custom">
    <div class="col-lg-12">
        <label id="lbError" runat="server" class="errMsg" visible="false"/>
    </div>
    <ul class="nav nav-tabs nav-tabs-custom">
        <li class="active">
            <a data-toggle="tab" href="#tabVN">Tiếng Việt</a>
        </li>
        <li>
            <a data-toggle="tab" href="#tabEN">English</a>
        </li>
    </ul>

    <div class="tab-content">
        <div id="tabVN" class="tab-pane fade in active">
            <br />
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="control-label col-lg-2">Tên:</label>
                    <asp:RequiredFieldValidator ID="rfvNameVN" runat="server" ControlToValidate="tbxNameVN" ErrorMessage="*" CssClass="required" SetFocusOnError="true" Display="Dynamic" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tbxNameVN" runat="server" CssClass="form-control form-control-custom" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-2">Ghi chú:</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="tbxDesVN" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control form-control-custom" />
                    </div>
                </div>
            </div>
        </div>

        <div id="tabEN" class="tab-pane fade">
            <br />
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="control-label col-lg-2">Name:</label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tbxNameEN" runat="server" CssClass="form-control form-control-custom" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-2">Description:</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="tbxDesEN" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control form-control-custom" />
                    </div>
                </div>
            </div>
        </div>

        <div class="form-horizontal">
            <div class="form-group">
                <label class="control-label col-lg-2">Link:</label>
                <div class="col-lg-10">
                    <asp:TextBox ID="tbxLink" runat="server" CssClass="form-control form-control-custom"></asp:TextBox>
                </div>
            </div>
            <div id="groupImages" runat="server" class="form-group">
                    <label class="control-label col-lg-2"></label>
                    <div id="project-detail-images" class="col-lg-10 project-detail-images">
                        <asp:ListView ID="lvProjectImages" runat="server" OnItemDataBound="lvProjectImages_ItemDataBound">
                            <ItemTemplate>
                                <div class="col-lg-3 project-detail-images-item">
                                    <asp:Image ID="itemImage" runat="server" />
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            <div class="form-group">
                <label class="control-label col-lg-2">Upload Ảnh:</label>
                <div class="col-lg-8">
                    <%--<div class="detail-image">
                        <asp:Image ID="imgImage" runat="server" Visible="false"/>
                    </div>--%>
                    <asp:FileUpload ID="fileUploadImage" runat="server" CssClass="form-control form-control-custom" multiple="multiple" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-lg-2"></div>
                <div class="col-lg-10">
                    <asp:Button ID="btnSubmit" runat="server" Text="Add" CssClass="btn btn-info btn-info-custom" ClientIDMode="Static" OnClick="btnSubmit_Click" ValidationGroup="Submit"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-danger-custom" OnClick="btnCancel_Click" Visible="false"/>
                </div>
            </div>

        </div>
    </div>
</div>