<%@ Page Title="" Language="C#" MasterPageFile="~/Ngo/NgoMasterPage.master" AutoEventWireup="true" CodeFile="CashReceipt.aspx.cs" Inherits="Ngo_CashReceipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Cash Receipt Page</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Cash Receipt Page</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <section class="content">
        <div class="container-fluid">
            <!-- SELECT2 EXAMPLE -->
            <div class="card card-default">
                <div class="card-header">
                    <h3 class="card-title">Cash Receipt</h3>
                </div>
                <!-- /.card-header -->
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="exampleInputAddress">Receipt Number</label><asp:Label ID="lblReceipt" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtReceiptId" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                          <div class="col-md-6">
                            <div class="form-group">
                                <label for="exampleInputAddress">Date</label>
                                <asp:TextBox ID="txtDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                            </div> 
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="exampleInputAddress">Donor Name</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtDonor" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtDonor" runat="server" placeholder="Donor Name" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputAddress">Payment Mode</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlPaymentMode" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="form-control">
                                    <asp:ListItem>--select type--</asp:ListItem>
                                    <asp:ListItem>Cash</asp:ListItem>
                                    <asp:ListItem>Other</asp:ListItem> 
                                </asp:DropDownList>
                            </div>
                        </div>
                      
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtAddress">Amount</label>
                                <asp:TextBox ID="txtAmount" runat="server" placeholder="Enter amount" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtAddress">Notes</label>
                                <asp:TextBox ID="txtNotes" runat="server" placeholder="" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <!-- /.col -->
                    </div>
                </div>
                <!-- /.card-body -->
                <div class="card-footer">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary float-right" OnClick="btnSubmit_Click" CausesValidation="true" />
                </div>
            </div>
        </div>
        <div class="col-12">
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title">Cash Receipt Data</h3>
                </div>
                <!-- /.card-header -->
                <div class="card-body table-responsive p-0">
                    <asp:Repeater ID="RepeaterUser" runat="server">
                        <HeaderTemplate>
                            <table class="table table-head-fixed">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>ReceiptNo</th>
                                        <th>DonorName</th>
                                        <th>DonationDate</th> 
                                        <th>PaymentMode</th>
                                        <th>Amount</th>
                                        <th>Notes</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Id")%></td>
                                <td><%# Eval("ReceiptNo")%></td>
                                <td><%# Eval("DonorName")%></td>
                                <td><%# Eval("DonationDate","{0:dd-MM-yyyy}")%></td> 
                                <td><%# Eval("PaymentMode")%></td>
                                <td><%# Eval("Amount")%></td>
                                <td><%# Eval("Notes")%></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                     </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <!-- /.card-body -->
            </div>
            <!-- /.card -->
        </div>
    </section>
</asp:Content>

