Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework

Public Class cRoleActions

    Sub AddNewRole(ByVal RoleName As String)
        Dim context As ApplicationDbContext = New ApplicationDbContext()
        Dim IdRoleResult As IdentityResult

        Dim roleStore = New RoleStore(Of IdentityRole)(context)
        Dim roleMgr = New RoleManager(Of IdentityRole)(roleStore)
        If Not roleMgr.RoleExists(RoleName) Then
            IdRoleResult = roleMgr.Create(New IdentityRole() With {.Name = RoleName})
        End If
    End Sub

    Public AddNewUserToRole_AspNetUserId As String
    Sub AddNewUserToRole(ByVal UserName As String, ByVal Role As String)
        Dim context As ApplicationDbContext = New ApplicationDbContext()
        Dim IdUserResult As IdentityResult

        Dim userMgr = New UserManager(Of ApplicationUser)(New UserStore(Of ApplicationUser)(context))
        If Not userMgr.IsInRole(userMgr.FindByName(UserName).Id, Role) Then
            AddNewUserToRole_AspNetUserId = userMgr.FindByName(UserName).Id
            IdUserResult = userMgr.AddToRole(AddNewUserToRole_AspNetUserId, Role)
        End If
    End Sub

    Function GetRolesAddNewUserToRole(ByVal UserName As String) As String
        Dim context As ApplicationDbContext = New ApplicationDbContext()

        Dim userMgr = New UserManager(Of ApplicationUser)(New UserStore(Of ApplicationUser)(context))
        Dim rRoles = userMgr.GetRoles(userMgr.FindByName(UserName).Id)

        Return rRoles(0).ToString
    End Function

    Sub AddUserAndRole()
        Dim context As ApplicationDbContext = New ApplicationDbContext()
        Dim IdRoleResult As IdentityResult
        Dim IdUserResult As IdentityResult

        Dim roleStore = New RoleStore(Of IdentityRole)(context)
        Dim roleMgr = New RoleManager(Of IdentityRole)(roleStore)
        If Not roleMgr.RoleExists("canEdit") Then
            IdRoleResult = roleMgr.Create(New IdentityRole() With {.Name = "canEdit"})
        End If

        Dim userMgr = New UserManager(Of ApplicationUser)(New UserStore(Of ApplicationUser)(context))
        Dim appUser = New ApplicationUser() With {.UserName = "canEditUser@wingtiptoys.com", .Email = "canEditUser@wingtiptoys.com"}
        IdUserResult = userMgr.Create(appUser, "Pa$$word1")

        If Not userMgr.IsInRole(userMgr.FindByEmail("canEditUser@wingtiptoys.com").Id, "canEdit") Then
            IdUserResult = userMgr.AddToRole(userMgr.FindByEmail("canEditUser@wingtiptoys.com").Id, "canEdit")
        End If
    End Sub

End Class
