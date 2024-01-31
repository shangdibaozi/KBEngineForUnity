namespace KBEngine
{
  	using UnityEngine; 
	using System; 
	using System.Collections; 
	using System.Collections.Generic;
	using System.Threading;

    /// <summary>
    /// KBE-Plugin fire-out events(KBE => Unity):
    /// </summary>
    public class EventOutTypes
    {
        // ------------------------------------账号相关------------------------------------

        /// <summary>
        /// Create account feedback results.
        /// <para> param1(uint16): retcode. // server_errors</para>
        /// <para> param2(bytes): datas. // If you use third-party account system, the system may fill some of the third-party additional datas. </para>
        /// </summary>
        public const string onCreateAccountResult = "onCreateAccountResult";

        /// <summary>
        // Response from binding account Email request.
        // <para> param1(uint16): retcode. // server_errors</para>
        /// </summary>
        public const string onBindAccountEmail = "onBindAccountEmail";

        /// <summary>
        // Response from a new password request.
        // <para> param1(uint16): retcode. // server_errors</para>
        /// </summary>
        public const string onNewPassword = "onNewPassword";

        /// <summary>
        // Response from a reset password request.
        // <para> param1(uint16): retcode. // server_errors</para>
        /// </summary>
        public const string onResetPassword = "onResetPassword";

        // ------------------------------------连接相关------------------------------------
        /// <summary>
        /// Kicked of the current server.
        /// <para> param1(uint16): retcode. // server_errors</para>
        /// </summary>
        public const string onKicked = "onKicked";

        /// <summary>
        /// Disconnected from the server.
        /// </summary>
        public const string onDisconnected = "onDisconnected";

        /// <summary>
        /// Status of connection server.
        /// <para> param1(bool): success or fail</para>
        /// </summary>
        public const string onConnectionState = "onConnectionState";

        // ------------------------------------logon相关------------------------------------
        /// <summary>
        /// Engine version mismatch.
        /// <para> param1(string): clientVersion
        /// <para> param2(string): serverVersion
        /// </summary>
        public const string onVersionNotMatch = "onVersionNotMatch";

        /// <summary>
        /// script version mismatch.
        /// <para> param1(string): clientScriptVersion
        /// <para> param2(string): serverScriptVersion
        /// </summary>
        public const string onScriptVersionNotMatch = "onScriptVersionNotMatch";

        /// <summary>
        /// Login failed.
        /// <para> param1(uint16): retcode. // server_errors</para>
        /// </summary>
        public const string onLoginFailed = "onLoginFailed";

        /// <summary>
        /// Login to baseapp.
        /// </summary>
        public const string onLoginBaseapp = "onLoginBaseapp";

        /// <summary>
        /// Login baseapp failed.
        /// <para> param1(uint16): retcode. // server_errors</para>
        /// </summary>
        public const string onLoginBaseappFailed = "onLoginBaseappFailed";

        /// <summary>
        /// Relogin to baseapp.
        /// </summary>
        public const string onReloginBaseapp = "onReloginBaseapp";

        /// <summary>
        /// Relogin baseapp success.
        /// </summary>
        public const string onReloginBaseappSuccessfully = "onReloginBaseappSuccessfully";

        /// <summary>
        /// Relogin baseapp failed.
        /// <para> param1(uint16): retcode. // server_errors</para>
        /// </summary>
        public const string onReloginBaseappFailed = "onReloginBaseappFailed";

        // ------------------------------------实体cell相关事件------------------------------------

        /// <summary>
        /// Entity enter the client-world.
        /// <para> param1: Entity</para>
        /// </summary>
        public const string onEnterWorld = "onEnterWorld";

        /// <summary>
        /// Entity leave the client-world.
        /// <para> param1: Entity</para>
        /// </summary>
        public const string onLeaveWorld = "onLeaveWorld";

        /// <summary>
        /// Player enter the new space.
        /// <para> param1: Entity</para>
        /// </summary>
        public const string onEnterSpace = "onEnterSpace";

        /// <summary>
        /// Player leave the space.
        /// <para> param1: Entity</para>
        /// </summary>
        public const string onLeaveSpace = "onLeaveSpace";

        /// <summary>
        /// Sets the current position of the entity.
        /// <para> param1: Entity</para>
        /// </summary>
        public const string set_position = "set_position";

        /// <summary>
        /// Sets the current direction of the entity.
        /// <para> param1: Entity</para>
        /// </summary>
        public const string set_direction = "set_direction";

        /// <summary>
        /// The entity position is updated, you can smooth the moving entity to new location.
        /// <para> param1: Entity</para>
        /// </summary>
        public const string updatePosition = "updatePosition";

        /// <summary>
        /// The current space is specified by the geometry mapping.
        /// Popular said is to load the specified Map Resources.
        /// <para> param1(string): resPath</para>
        /// </summary>
        public const string addSpaceGeometryMapping = "addSpaceGeometryMapping";

        /// <summary>
        /// Server spaceData set data.
        /// <para> param1(int32): spaceID</para>
        /// <para> param2(string): key</para>
        /// <para> param3(string): value</para>
        /// </summary>
        public const string onSetSpaceData = "onSetSpaceData";

        /// <summary>
        /// Start downloading data.
        /// <para> param1(int32): rspaceID</para>
        /// <para> param2(string): key</para>
        /// </summary>
        public const string onDelSpaceData = "onDelSpaceData";

        /// <summary>
        /// Triggered when the entity is controlled or out of control.
        /// <para> param1: Entity</para>
        /// <para> param2(bool): isControlled</para>
        /// </summary>
        public const string onControlled = "onControlled";

        /// <summary>
        /// Lose controlled entity.
        /// <para> param1: Entity</para>
        /// </summary>
        public const string onLoseControlledEntity = "onLoseControlledEntity";

        // ------------------------------------数据下载相关------------------------------------
        /// <summary>
        /// Start downloading data.
        /// <para> param1(uint16): resouce id</para>
        /// <para> param2(uint32): data size</para>
        /// <para> param3(string): description</para>
        /// </summary>
        public const string onStreamDataStarted = "onStreamDataStarted";

        /// <summary>
        /// Receive data.
        /// <para> param1(uint16): resouce id</para>
        /// <para> param2(bytes): datas</para>
        /// </summary>
        public const string onStreamDataRecv = "onStreamDataRecv";

        /// <summary>
        /// The downloaded data is completed.
        /// <para> param1(uint16): resouce id</para>
        /// </summary>
        public const string onStreamDataCompleted = "onStreamDataCompleted";
    };

    /// <summary>
    /// KBE-Plugin fire-in events(Unity => KBE):
    /// </summary>
    public class EventInTypes
    {
        /// <summary>
        /// Create new account.
        /// <para> param1(string): accountName</para>
        /// <para> param2(string): password</para>
        /// <para> param3(bytes): datas // Datas by user defined. Data will be recorded into the KBE account database, you can access the datas through the script layer. If you use third-party account system, datas will be submitted to the third-party system.</para>
        /// </summary>
        public const string createAccount = "createAccount";

        /// <summary>
        /// Login to server.
        /// <para> param1(string): accountName</para>
        /// <para> param2(string): password</para>
        /// <para> param3(bytes): datas // Datas by user defined. Data will be recorded into the KBE account database, you can access the datas through the script layer. If you use third-party account system, datas will be submitted to the third-party system.</para>
        /// </summary>
        public const string login = "login";

        /// <summary>
        /// Logout to baseapp, called when exiting the client.
        /// </summary>
        public const string logout = "logout";

        /// <summary>
        /// Relogin to baseapp.
        /// </summary>
        public const string reloginBaseapp = "reloginBaseapp";

        /// <summary>
        /// Reset password.
        /// <para> param1(string): accountName</para>
        /// </summary>
        public const string resetPassword = "resetPassword";

        /// <summary>
        /// Request to set up a new password for the account. Note: account must be online.
        /// <para> param1(string): old_password</para>
        /// <para> param2(string): new_password</para>
        /// </summary>
        public const string newPassword = "newPassword";

        /// <summary>
        /// Request server binding account Email.
        /// <para> param1(string): emailAddress</para>
        /// </summary>
        public const string bindAccountEmail = "bindAccountEmail";
    };
} 
