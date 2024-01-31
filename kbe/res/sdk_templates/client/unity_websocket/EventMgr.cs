using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace KBEngine
{
    public class EventMgr
    {
        private static readonly Dictionary<int, List<string>> instanceId2Key = new Dictionary<int, List<string>>();

        private static readonly Dictionary<string, Delegate>
            uniqueKey2Callback = new Dictionary<string, Delegate>();

        private static readonly Dictionary<string, List<string>> key2UniqueKey =
            new Dictionary<string, List<string>>();

        private static readonly Dictionary<string, int> uniqueKey2ArgCount = new Dictionary<string, int>();

        #region Register

        public static void Register(string key, object target, Action callback)
        {
            _RegisterEvent(key, 0, target, callback);
        }

        public static void Register<T1>(string key, object target, Action<T1> callback)
        {
            _RegisterEvent(key, 1, target, callback);
        }

        public static void Register<T1, T2>(string key, object target, Action<T1, T2> callback)
        {
            _RegisterEvent(key, 2, target, callback);
        }

        public static void Register<T1, T2, T3>(string key, object target, Action<T1, T2, T3> callback)
        {
            _RegisterEvent(key, 3, target, callback);
        }

        public static void Register<T1, T2, T3, T4>(string key, object target, Action<T1, T2, T3, T4> callback)
        {
            _RegisterEvent(key, 4, target, callback);
        }

        public static void Register<T1, T2, T3, T4, T5>(string key, object target,
            Action<T1, T2, T3, T4, T5> callback)
        {
            _RegisterEvent(key, 5, target, callback);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void _RegisterEvent(string key, int argCount, object target, Delegate callback)
        {
            string uniqueKey = GetUniqueKey(key, target);

            if (uniqueKey2Callback.ContainsKey(uniqueKey)) // 已经注册过该事件
            {
                return;
            }

            uniqueKey2Callback[uniqueKey] = callback;
            uniqueKey2ArgCount[uniqueKey] = argCount;

            if (!key2UniqueKey.ContainsKey(key))
            {
                key2UniqueKey.Add(key, new List<string>());
            }

            key2UniqueKey[key].Add(uniqueKey);

            int targetId = target.GetHashCode();
            if (!instanceId2Key.ContainsKey(targetId))
            {
                instanceId2Key.Add(targetId, new List<string>());
            }

            instanceId2Key[targetId].Add(key);
        }

        #endregion

        #region Deregister

        public static void Deregister(string key, object target)
        {
            _Deregister(key, target);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void _Deregister(string key, object target)
        {
            int targetId = target.GetHashCode();
            string uniqueKey = GetUniqueKey(key, target);
            if (!uniqueKey2Callback.ContainsKey(uniqueKey))
            {
                return;
            }

            uniqueKey2Callback.Remove(uniqueKey);
            uniqueKey2ArgCount.Remove(uniqueKey);
            key2UniqueKey[key].Remove(uniqueKey);
            instanceId2Key[targetId].Remove(key);

            if (key2UniqueKey[key].Count == 0)
            {
                key2UniqueKey.Remove(key);
            }

            if (instanceId2Key[targetId].Count == 0)
            {
                instanceId2Key.Remove(targetId);
            }
        }

        public static void DeregisterAll(object target)
        {
            int targetId = target.GetHashCode();
            if (instanceId2Key.TryGetValue(targetId, out var nameOfEvents))
            {
                foreach (string key in nameOfEvents)
                {
                    string uniqueKey = GetUniqueKey(key, target);
                    uniqueKey2Callback.Remove(uniqueKey);
                    uniqueKey2ArgCount.Remove(uniqueKey);
                    key2UniqueKey[key].Remove(uniqueKey);
                    if (key2UniqueKey[key].Count == 0)
                    {
                        key2UniqueKey.Remove(key);
                    }
                }
            }

            instanceId2Key.Remove(targetId);
        }

        #endregion

        #region Fire

        // 分发事件时，可以向参数个数小于分发时参数个数的回调发送，不能向多于分发参数个数的回调发送事件，因为不知道多余的数据类型是什么。
        public static void Fire<T1, T2, T3, T4, T5>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            _Fire(key, 5, arg1, arg2, arg3, arg4, arg5);
        }

        public static void Fire<T1, T2, T3, T4>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            _Fire(key, 4, arg1, arg2, arg3, arg4, arg4);
        }

        public static void Fire<T1, T2, T3>(string key, T1 arg1, T2 arg2, T3 arg3)
        {
            _Fire(key, 3, arg1, arg2, arg3, arg3, arg3);
        }

        public static void Fire<T1, T2>(string key, T1 arg1, T2 arg2)
        {
            _Fire(key, 2, arg1, arg2, arg2, arg2, arg2);
        }

        public static void Fire<T>(string key, T arg1)
        {
            _Fire(key, 1, arg1, arg1, arg1, arg1, arg1);
        }

        public static void Fire(string key)
        {
            if (key2UniqueKey.TryGetValue(key, out var nameOfEvents))
            {
                for (int i = nameOfEvents.Count - 1; i >= 0; i--)
                {
                    var uniqueKey = nameOfEvents[i];
                    var callback = uniqueKey2Callback[uniqueKey];
                    int argCount = uniqueKey2ArgCount[uniqueKey];
                    if (argCount != 0)
                    {
                        throw new Exception($"callback arg count not zero! {uniqueKey}");
                    }
                    else
                    {
                        ((Action)callback)();
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void _Fire<T1, T2, T3, T4, T5>(string key, int realArgCount, T1 arg1, T2 arg2, T3 arg3,
            T4 arg4, T5 arg5)
        {
            if (key2UniqueKey.TryGetValue(key, out List<string> nameOfEvents))
            {
                for (int i = nameOfEvents.Count - 1; i >= 0; i--)
                {
                    var uniqueKey = nameOfEvents[i];
                    var callback = uniqueKey2Callback[uniqueKey];
                    int argCount = uniqueKey2ArgCount[uniqueKey];
                    if (argCount > realArgCount)
                    {
                        throw new Exception($"callback arg count > {realArgCount}. {uniqueKey}");
                    }

                    switch (argCount)
                    {
                        case 0:
                            ((Action)callback)();
                            break;
                        case 1:
                            ((Action<T1>)callback)(arg1);
                            break;
                        case 2:
                            ((Action<T1, T2>)callback)(arg1, arg2);
                            break;
                        case 3:
                            ((Action<T1, T2, T3>)callback)(arg1, arg2, arg3);
                            break;
                        case 4:
                            ((Action<T1, T2, T3, T4>)callback)(arg1, arg2, arg3, arg4);
                            break;
                        case 5:
                            ((Action<T1, T2, T3, T4, T5>)callback)(arg1, arg2, arg3, arg4, arg5);
                            break;
                    }
                }
            }
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string GetUniqueKey(string key, object target)
        {
            return $"{target}_{target.GetHashCode()}_{key}";
        }

    }
}