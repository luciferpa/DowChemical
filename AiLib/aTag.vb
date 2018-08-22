Imports System.Text.RegularExpressions

Public Class aTag
    Public Shared Function chkTagDetail(ByVal TagDetail As String)
        Dim tag0 As String = ""
        Dim keyword As String = ""
        Dim tagString As String = TagDetail
        Dim firstStr As Char
        Dim chk As Regex = New Regex("[^a-zA-Z0-9ก-๙-_+]")
        '[a-zA-Z0-9\\p{L}\\p{M}-_]+
        '^[0-9a-zA-Zก-๙-_]+
        Dim i As Integer = tagString.IndexOf(",")

        If i > 0 Then
            While (i <> -1)
                keyword = tagString.Substring(0, tagString.IndexOf(",")).Trim()
                tagString = tagString.Substring(tagString.IndexOf(",") + 1).Trim()
                firstStr = tagString.Chars(0)
                'firstStr = tagString.Substring(0, 1)

                While (chk.IsMatch(firstStr))
                    tagString = tagString.Substring(1).Trim()
                    firstStr = tagString.Chars(0)
                    'firstStr = tagString.Substring(0, 1)
                End While

                i = tagString.IndexOf(",")
                If i <> -1 Then
                    tag0 = tag0 + keyword + ", "
                Else
                    tag0 = tag0 + keyword + ", " + tagString
                End If
            End While
        Else
            If i = 0 Then
                tagString = tagString.Substring(1).Trim()
            End If
            tag0 = tagString
        End If

        Return tag0
    End Function

End Class
