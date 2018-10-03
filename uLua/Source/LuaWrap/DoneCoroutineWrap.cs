﻿using System;
using LuaInterface;

public class DoneCoroutineWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateDoneCoroutine),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("isDoneCoroutine", get_isDoneCoroutine, set_isDoneCoroutine),
		};

		LuaScriptMgr.RegisterLib(L, "DoneCoroutine", typeof(DoneCoroutine), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateDoneCoroutine(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			DoneCoroutine obj = new DoneCoroutine();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: DoneCoroutine.New");
		}

		return 0;
	}

	static Type classType = typeof(DoneCoroutine);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isDoneCoroutine(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DoneCoroutine obj = (DoneCoroutine)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isDoneCoroutine");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isDoneCoroutine on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isDoneCoroutine);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isDoneCoroutine(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DoneCoroutine obj = (DoneCoroutine)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isDoneCoroutine");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isDoneCoroutine on a nil value");
			}
		}

		obj.isDoneCoroutine = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}
}

