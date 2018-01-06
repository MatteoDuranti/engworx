Namespace AppCode.CustomProvider
    Public Interface IFormsAuthentication
        Sub SignIn(username As String)
        Sub SignIn(company As String, username As String)
        Sub SignOut()
    End Interface
End Namespace