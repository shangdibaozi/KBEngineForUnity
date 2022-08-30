#pragma once

#include "client_sdk_unity.h"

namespace KBEngine
{
	class ClientSDKUnityWebSocket : public ClientSDKUnity
	{
		virtual std::string name() const {
			return "unity_websocket";
		}
	};
}