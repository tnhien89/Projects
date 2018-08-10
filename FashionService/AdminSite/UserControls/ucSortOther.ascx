<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucSortOther.ascx.cs" Inherits="AdminSite.UserControls.ucSortOther" %>

<style>
    .other-listbox
    {
        max-height: 500px;
    }

    .other-type-sort-button-group
    {
        top: 0;
        bottom: 0;
        left: 0;
        right: 0;
        margin: auto;
        position: absolute;
    }

        .other-type-sort-button-group input[type=button]
        {
            width: 60px;
            margin: 5px;
            display: block;
        }
</style>

<div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <h4 class="modal-title">Sắp xếp</h4>
        </div>
        <div class="modal-body">
            <div class="col-lg-12">
                <label id="lbError" runat="server" class="errMsg" hidden="hidden"></label>
            </div>
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-lg-10">
                        <asp:ListBox ID="lbxOther" runat="server" ClientIDMode="Static" CssClass="form-control other-listbox"></asp:ListBox>
                    </div>
                    <div class="col-lg-2">
                        <div class="other-type-sort-button-group">
                            <input type="button" id="btnUp" value="Up" class="form-control btn btn-danger btn-danger-custom" disabled="disabled"/>
                            <input type="button" id="btnDown" value="Down" class="form-control btn btn-danger btn-danger-custom" disabled="disabled"/>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-lg-12">
                        <input type="button" id="btnSortSubmit" value="Submit" class="btn btn-info btn-info-custom"/>
                    </div>
                </div>
            </div>

        </div>
    </div>
</div>

<script type="text/javascript">
    $("#lbxOther").on('change', function () {
        $('#btnUp').prop('disabled', false);
        $('#btnDown').prop('disabled', false);
    });

    $('#btnUp').on('click', function () {
        startSortOtherType('Up');
    });

    $('#btnDown').on('click', function () {
        startSortOtherType('Down');
    });

    function startSortOtherType(val) {
        var $op = $('#lbxOther option:selected');

        if (val === 'Up') {
            $op.first().prev().before($op);
        }
        else {
            $op.last().next().after($op);
        }
    }

    $('#btnSortSubmit').on('click', function () {
        var data = [];
        var idx = 0;
        $('#lbxOther option').each(function () {
            debugger;
            var id = $(this).val();
            var item = {
                'Id': id,
                'Index': idx
            };

            data.push(item);
            idx += 1;
        });

        if (data && data.length)
        {
            $.ajax({
                url: 'WebHandler/SortOtherTypeHandler.ashx',
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                data: JSON.stringify(data),
                success: function (result) {
                    if (result !== undefined) {
                        if (result.Code < 0) {
                            // error
                            alert(result.Message);
                        }
                        else {
                            //$("#ddlSubMenu").append(
                            //    $("<option></option>").val(result.Id).html(result.Name));

                            location.reload();
                        }
                    }
                },
                error: function (xhr, status, error) {
                    alert("Error code: " + xhr.status + " - " + error);
                }
            });
        }
    });
</script>