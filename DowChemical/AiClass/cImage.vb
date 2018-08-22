Public Class cImage

    Public Function getImageURL(ByVal status As Integer, ByVal item As Integer) As String
        Dim ImgURL As String = ""
        Select Case status
            Case 1000
                ImgURL = "~/Images/status-blank-20.png"
            Case 1001
                ImgURL = "~/Images/status-blue-" & item.ToString & "-20.png"
            Case 1002
                ImgURL = "~/Images/status-orange-" & item.ToString & "-20.png"
            Case 1003
                ImgURL = "~/Images/status-green-" & item.ToString & "-20.png"
        End Select

        Return ImgURL
    End Function

    Public Function getImage_status(ByVal status As Integer, ByVal item As Integer) As String
        Dim ImgURL As String = ""
        Select Case status
            Case 1000
                ImgURL = "~/Images/status-blank-20.png"
            Case 1001
                ImgURL = "~/Images/status-blue-" & item.ToString & "-20.png"
            Case 1002
                ImgURL = "~/Images/status-orange-" & item.ToString & "-20.png"
            Case 1003
                ImgURL = "~/Images/status-green-" & item.ToString & "-20.png"
        End Select

        Return ImgURL
    End Function

    Public getImage_msgTooltip As String
    Public Function getImage_msg(ByVal status As Integer, ByVal item As Integer) As String
        Dim ImgURL As String = ""
        Select Case status
            Case 1000
                ImgURL = "~/Images/status-blank-20.png"
                getImage_msgTooltip = ""
            Case 1001
                ImgURL = "~/Images/msg-blue-" & item.ToString & "-20.png"
                getImage_msgTooltip = "Recognition"
            Case 1002
                ImgURL = "~/Images/msg-orange-" & item.ToString & "-20.png"
                getImage_msgTooltip = "In-progress"
            Case 1003
                ImgURL = "~/Images/msg-green-" & item.ToString & "-20.png"
                getImage_msgTooltip = "Complete"
        End Select

        Return ImgURL
    End Function
End Class
