<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        RegisterRoutes(RouteTable.Routes);
    }

    static void RegisterRoutes(RouteCollection routes)
    {
        routes.MapPageRoute("Home", "Home", "~/Home.aspx");
        routes.MapPageRoute("Register", "Register", "~/Register_Member_Share_Link.aspx");
        routes.MapPageRoute("Login", "Login", "~/Login.aspx");
        routes.MapPageRoute("InviteFriend", "InviteFriend", "~/InviteFriend.aspx");
        routes.MapPageRoute("ComingSoon", "ComingSoon", "~/ComingSoon.aspx");
        routes.MapPageRoute("InviteFriend_Now", "InviteFriend_Now", "~/InviteFriend_Now.aspx");
        routes.MapPageRoute("AssignMember", "AssignMember", "~/AssignMember.aspx");
        routes.MapPageRoute("MemberNetwork", "MemberNetwork", "~/Member_Network.aspx");
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
