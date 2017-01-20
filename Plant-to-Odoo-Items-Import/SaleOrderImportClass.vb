Imports FileHelpers

<DelimitedRecord(",")>
<IgnoreFirst>
Public Class SaleOrderImportClass

    Public id_unicommerce As String
    Public order_code As String
    Public email As String
    Public mobile As String
    Public sa_name As String
    <FieldQuoted(""""c, QuoteMode.OptionalForRead)>
    Public sa_add_1 As String
    <FieldQuoted(""""c, QuoteMode.OptionalForRead)>
    Public sa_add_2 As String
    Public sa_city As String
    Public sa_state As String
    Public sa_zip As String
    Public ba_state As String
    Public ba_zip As String
    Public item_sku As String
    Public item_name As String
    Public chanel_name As String
    Public selling_price As Integer
    Public sale_order_code As String
    Public sale_journal As String
    Public typology As String
    Public sale_tax_code As String
    Public customer As String

End Class
