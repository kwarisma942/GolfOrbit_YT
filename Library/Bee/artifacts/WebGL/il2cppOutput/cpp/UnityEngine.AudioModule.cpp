﻿#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>


template <typename T1>
struct VirtualActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename T1>
struct GenericVirtualActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (const RuntimeMethod* method, RuntimeObject* obj, T1 p1)
	{
		VirtualInvokeData invokeData;
		il2cpp_codegen_get_generic_virtual_invoke_data(method, obj, &invokeData);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename T1>
struct InterfaceActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename T1>
struct GenericInterfaceActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (const RuntimeMethod* method, RuntimeObject* obj, T1 p1)
	{
		VirtualInvokeData invokeData;
		il2cpp_codegen_get_generic_interface_invoke_data(method, obj, &invokeData);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};

struct AudioMixerGroupU5BU5D_t8FDE37A00A7E874EB75184A6133A160137D0532B;
struct AudioMixerSnapshotU5BU5D_tD41A94680B8A241063054F1931BDDC8B997D8382;
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
struct SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C;
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;
struct Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07;
struct ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263;
struct AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20;
struct AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04;
struct AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311;
struct AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781;
struct AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2;
struct AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299;
struct Delegate_t;
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
struct MethodInfo_t;
struct NullReferenceException_tBDE63A6D24569B964908408389070C6A9F5005BB;
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C;
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
struct String_t;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
struct PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E;
struct PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072;
struct SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30;
struct AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177;

IL2CPP_EXTERN_C RuntimeClass* ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* NullReferenceException_tBDE63A6D24569B964908408389070C6A9F5005BB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D;
IL2CPP_EXTERN_C String_t* _stringLiteral49403A17E8D32B35CB5B66AB1A2651A7EEAD1B00;
IL2CPP_EXTERN_C String_t* _stringLiteral5633EC5A323E76AB2DB48162D296F674B1A4AE13;
IL2CPP_EXTERN_C String_t* _stringLiteral747CA643504D60A83574C1454D344DADB4D2BA17;
IL2CPP_EXTERN_C String_t* _stringLiteral8C53570A90E7EFA87FDB7F0DCC0EE1FCC019E426;
IL2CPP_EXTERN_C String_t* _stringLiteral9EAFE4F84FECC3407E28F6C22DC663D4A31C78D0;
IL2CPP_EXTERN_C String_t* _stringLiteralA69BE8EF0F1482795ABF1CC395A765AA747ECD85;
IL2CPP_EXTERN_C String_t* _stringLiteralB1FAE4F904170535BC9DC23FF347B1D0603313EA;
IL2CPP_EXTERN_C String_t* _stringLiteralB9ACF8E6DF73A863387F2ABAD61D84C3197C6B01;
IL2CPP_EXTERN_C String_t* _stringLiteralC01DE08865A1C5D9A0B7F1023D8CD55C8685904B;
IL2CPP_EXTERN_C String_t* _stringLiteralC563ED6549A22286FAAA38A5D49C5B247A52C622;
IL2CPP_EXTERN_C String_t* _stringLiteralCC867B94415511BD48E92E3242D7EDA425D20035;
IL2CPP_EXTERN_C String_t* _stringLiteralF52B980B71426E2D226C2ED5B2045868E72F85FF;
IL2CPP_EXTERN_C const RuntimeMethod* AudioClip_Create_m988FEB04BC74440E65C3CF07414E4867AAE737F8_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioClip_SetData_m7B473C614C11953D746770F4F89B44600B5A6AF3_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioMixer_TransitionToSnapshot_m308E8D50A4573EFDD381ED7DEC23162F8CD0EB5D_RuntimeMethod_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;

struct AudioMixerGroupU5BU5D_t8FDE37A00A7E874EB75184A6133A160137D0532B;
struct AudioMixerSnapshotU5BU5D_tD41A94680B8A241063054F1931BDDC8B997D8382;
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C;
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct U3CModuleU3E_t462BCCFB9B78348533823E0754F65F52A5348F89 
{
};
struct AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2  : public RuntimeObject
{
	SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* ___sampleFramesAvailable;
	SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* ___sampleFramesOverflow;
};
struct AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD  : public RuntimeObject
{
};
struct String_t  : public RuntimeObject
{
	int32_t ____stringLength;
	Il2CppChar ____firstChar;
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	bool ___m_value;
};
struct Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F 
{
	double ___m_value;
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2  : public ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_pinvoke
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_com
{
};
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	int32_t ___m_value;
};
struct IntPtr_t 
{
	void* ___m_value;
};
struct Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C 
{
	float ___m_value;
};
struct UInt32_t1833D51FFA667B18A5AA4B8D34DE284F8495D29B 
{
	uint32_t ___m_value;
};
struct UInt64_t8F12534CC8FC4B5860F2A2CD1EE79D322E7A41AF 
{
	uint64_t ___m_value;
};
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};
struct AudioClipLoadType_t9FDAF1B7675E8608120B797D440340BCEE11160C 
{
	int32_t ___value__;
};
struct AudioDataLoadState_tC19F28D3D0EFE5FD5A6F6B0DBC2253A487A7DC46 
{
	int32_t ___value__;
};
struct AudioMixerUpdateMode_tC3F444582B5A55F4D4FC0DDF3D17F834EFACB9F8 
{
	int32_t ___value__;
};
struct Delegate_t  : public RuntimeObject
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	RuntimeObject* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	bool ___method_is_virtual;
};
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	Il2CppIUnknown* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	int32_t ___method_is_virtual;
};
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	Il2CppIUnknown* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	int32_t ___method_is_virtual;
};
struct Exception_t  : public RuntimeObject
{
	String_t* ____className;
	String_t* ____message;
	RuntimeObject* ____data;
	Exception_t* ____innerException;
	String_t* ____helpURL;
	RuntimeObject* ____stackTrace;
	String_t* ____stackTraceString;
	String_t* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	RuntimeObject* ____dynamicMethods;
	int32_t ____HResult;
	String_t* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_pinvoke
{
	char* ____className;
	char* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_pinvoke* ____innerException;
	char* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	char* ____stackTraceString;
	char* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	char* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_com
{
	Il2CppChar* ____className;
	Il2CppChar* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_com* ____innerException;
	Il2CppChar* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	Il2CppChar* ____stackTraceString;
	Il2CppChar* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	Il2CppChar* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C  : public RuntimeObject
{
	intptr_t ___m_CachedPtr;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
	intptr_t ___m_CachedPtr;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
	intptr_t ___m_CachedPtr;
};
struct PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 
{
	intptr_t ___m_Handle;
	uint32_t ___m_Version;
};
struct PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 
{
	intptr_t ___m_Handle;
	uint32_t ___m_Version;
};
struct AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
	PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* ___m_PCMReaderCallback;
	PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* ___m_PCMSetPositionCallback;
};
struct AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 
{
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___m_Handle;
};
struct AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C 
{
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___m_Handle;
};
struct AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct AudioPlayableOutput_tC3DFF8095F429D90129A367EAB98A24F6D6ADF20 
{
	PlayableOutputHandle_tEB217645A8C0356A3AC6F964F283003B9740E883 ___m_Handle;
};
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct MulticastDelegate_t  : public Delegate_t
{
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* ___delegates;
};
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates;
};
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates;
};
struct SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295  : public Exception_t
{
};
struct Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07  : public MulticastDelegate_t
{
};
struct ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
	String_t* ____paramName;
};
struct Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};
struct NullReferenceException_tBDE63A6D24569B964908408389070C6A9F5005BB  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};
struct PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E  : public MulticastDelegate_t
{
};
struct PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072  : public MulticastDelegate_t
{
};
struct SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30  : public MulticastDelegate_t
{
};
struct AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177  : public MulticastDelegate_t
{
};
struct AudioBehaviour_t2DC0BEF7B020C952F3D2DA5AAAC88501C7EEB941  : public Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA
{
};
struct AudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35  : public AudioBehaviour_t2DC0BEF7B020C952F3D2DA5AAAC88501C7EEB941
{
};
struct AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299  : public AudioBehaviour_t2DC0BEF7B020C952F3D2DA5AAAC88501C7EEB941
{
};
struct AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_StaticFields
{
	AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* ___OnAudioConfigurationChanged;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___OnAudioSystemShuttingDown;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___OnAudioSystemStartedUp;
};
struct String_t_StaticFields
{
	String_t* ___Empty;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	String_t* ___TrueString;
	String_t* ___FalseString;
};
struct IntPtr_t_StaticFields
{
	intptr_t ___Zero;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_StaticFields
{
	int32_t ___OffsetOfInstanceIDInCPlusPlusObject;
};
struct PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_StaticFields
{
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___m_Null;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771  : public RuntimeArray
{
	ALIGN_FIELD (8) Delegate_t* m_Items[1];

	inline Delegate_t* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Delegate_t** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Delegate_t* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline Delegate_t* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Delegate_t** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Delegate_t* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
struct SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C  : public RuntimeArray
{
	ALIGN_FIELD (8) float m_Items[1];

	inline float GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline float* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, float value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline float GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline float* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, float value)
	{
		m_Items[index] = value;
	}
};
struct AudioMixerGroupU5BU5D_t8FDE37A00A7E874EB75184A6133A160137D0532B  : public RuntimeArray
{
	ALIGN_FIELD (8) AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311* m_Items[1];

	inline AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248  : public RuntimeArray
{
	ALIGN_FIELD (8) String_t* m_Items[1];

	inline String_t* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline String_t** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, String_t* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline String_t* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline String_t** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, String_t* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
struct AudioMixerSnapshotU5BU5D_tD41A94680B8A241063054F1931BDDC8B997D8382  : public RuntimeArray
{
	ALIGN_FIELD (8) AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781* m_Items[1];

	inline AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};



IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_inline (AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, bool ___0_deviceWasChanged, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_m2149FA40CEC8D82AC20D3508AB40C0D8EFEF68E6 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AudioClip_get_channels_mFEECF5D6389D196BA5102EB79257298B9FDC9F84 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AudioClip_GetName_m561BBA037957E25D5BC5A962A1AA0C789895C9D1 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B (String_t* ___0_str0, String_t* ___1_str1, String_t* ___2_str2, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_Log_m87A9A3C761FF5C43ED8A53B16190A53D08F818BB (RuntimeObject* ___0_message, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClip_GetData_mBDEFD7D7C8E5DEA3CCEE2D7DB406DBB0C244924E (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_clip, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___1_data, int32_t ___2_numSamples, int32_t ___3_samplesOffset, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AudioClip_get_samples_mDEA01CA75E7DEA0F8D480E4AF97FB96085BCF38E (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465 (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* __this, String_t* ___0_message, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClip_SetData_mB49A9BC4639C62B9C8B22319D33D46AAD176BC3B (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_clip, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___1_data, int32_t ___2_numsamples, int32_t ___3_samplesOffset, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* AudioClip_Create_mE8111F06981E42666B6A9A59D0A3EBE002D2CDFB (String_t* ___0_name, int32_t ___1_lengthSamples, int32_t ___2_channels, int32_t ___3_frequency, bool ___4_stream, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* AudioClip_Create_m988FEB04BC74440E65C3CF07414E4867AAE737F8 (String_t* ___0_name, int32_t ___1_lengthSamples, int32_t ___2_channels, int32_t ___3_frequency, bool ___4_stream, PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* ___5_pcmreadercallback, PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* ___6_pcmsetpositioncallback, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NullReferenceException__ctor_mD26D62094A5E49C18D817817E17FDFBC1D3BD752 (NullReferenceException_tBDE63A6D24569B964908408389070C6A9F5005BB* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* AudioClip_Construct_Internal_m88BC07CE3F412DDB62820F9430D1D52DA42A26F6 (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClip_add_m_PCMReaderCallback_mA226EA143D90E04117A740FC9FA9F1111346CA83 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClip_add_m_PCMSetPositionCallback_mB280AD93A847C65F536D846FECC7DCBE9266C37F (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClip_CreateUserSound_m34DA102DD6848D555D4A9D45AFAA9D3E5574BC45 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, String_t* ___0_name, int32_t ___1_lengthSamples, int32_t ___2_channels, int32_t ___3_frequency, bool ___4_stream, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Delegate_t* Delegate_Combine_m1F725AEF318BE6F0426863490691A6F4606E7D00 (Delegate_t* ___0_a, Delegate_t* ___1_b, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Delegate_t* Delegate_Remove_m8B7DD5661308FA972E23CA1CC3FC9CEB355504E3 (Delegate_t* ___0_source, Delegate_t* ___1_value, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_inline (PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_inline (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, int32_t ___0_position, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AudioSource_GetPitch_m80F6D2BAF966F669253E9231AFCFFC303779913D (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_source, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_SetPitch_mE75DEDF8F37301BDA63E0F545A7A00850C24F53E (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_source, float ___1_pitch, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayHelper_m4DE8C48925C3548BED306DAB9F87939F24A46960 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_source, uint64_t ___1_delay, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_Play_m10DB5ACD1CC32EE433DBC10416B1450A30DE5F16 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, double ___0_delay, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_x, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___1_y, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9 (RuntimeObject* ___0_message, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayOneShotHelper_mD110EAF42353687BD0B1190EEF30F0C65A4CF265 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_source, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___1_clip, float ___2_volumeScale, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_inline (SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 AudioClipPlayable_GetHandle_mEA1D664328FF9B08E4F7D5EBCD4B51A754D97C44 (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool PlayableHandle_op_Equality_m0E6C48A28F75A870AC22ADE3BD42F7F70A43C99C (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___0_x, PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___1_y, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClipPlayable_Equals_m9C1C75ACBB74FE06AD02BE4643F6EB39413EFF83 (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* __this, AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 ___0_other, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Object_get_name_mAC2F6B897CF1303BA4249B4CB55271AFACBB6392 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* AudioMixerSnapshot_get_audioMixer_mCC13199F7F9EBB2C5510DFA750A501747229CBF5 (AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Inequality_mD0BE578448EAA61948F25C32F8DD55AB1F778602 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_x, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___1_y, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m647EBF831F54B6DF7D5AFA5FD012CF4EE7571B6A (StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* ___0_values, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioMixer_TransitionToSnapshotInternal_m9D07330A3EF997CC4BB165829D8D2AD6187A8DD0 (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* __this, AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781* ___0_snapshot, float ___1_timeToReach, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 AudioMixerPlayable_GetHandle_m6C182D9794E901D123223BB57738A302BEAB41FD (AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioMixerPlayable_Equals_mDFB945EB48199A338BAD00D40FB8EEC34CF64D57 (AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C* __this, AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C ___0_other, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 60154
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSettings_InvokeOnAudioConfigurationChanged_m8273D3AEB24F4C3E374238B6F699BE6696808E85 (bool ___0_deviceWasChanged, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	{
		AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* L_0 = ((AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_StaticFields*)il2cpp_codegen_static_fields_for(AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var))->___OnAudioConfigurationChanged;
		V_0 = (bool)((!(((RuntimeObject*)(AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177*)L_0) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_0019;
		}
	}
	{
		AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* L_2 = ((AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_StaticFields*)il2cpp_codegen_static_fields_for(AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var))->___OnAudioConfigurationChanged;
		bool L_3 = ___0_deviceWasChanged;
		AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_inline(L_2, L_3, NULL);
	}

IL_0019:
	{
		return;
	}
}
// Method Definition Index: 60155
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSettings_InvokeOnAudioSystemShuttingDown_m1B9895D60B3267EBDEC69B9169730DBAD8325E90 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* G_B2_0 = NULL;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* G_B1_0 = NULL;
	{
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_0 = ((AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_StaticFields*)il2cpp_codegen_static_fields_for(AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var))->___OnAudioSystemShuttingDown;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000b;
		}
		G_B1_0 = L_1;
	}
	{
		goto IL_0011;
	}

IL_000b:
	{
		Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline(G_B2_0, NULL);
	}

IL_0011:
	{
		return;
	}
}
// Method Definition Index: 60156
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSettings_InvokeOnAudioSystemStartedUp_m7FE042936237E5BDCB20299D8C4CF583B661468C (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* G_B2_0 = NULL;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* G_B1_0 = NULL;
	{
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_0 = ((AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_StaticFields*)il2cpp_codegen_static_fields_for(AudioSettings_t66C4BCA1E463B061E2EC9063FB882ACED20D47BD_il2cpp_TypeInfo_var))->___OnAudioSystemStartedUp;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000b;
		}
		G_B1_0 = L_1;
	}
	{
		goto IL_0011;
	}

IL_000b:
	{
		Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline(G_B2_0, NULL);
	}

IL_0011:
	{
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
void AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_Multicast(AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, bool ___0_deviceWasChanged, const RuntimeMethod* method)
{
	il2cpp_array_size_t length = __this->___delegates->max_length;
	Delegate_t** delegatesToInvoke = reinterpret_cast<Delegate_t**>(__this->___delegates->GetAddressAtUnchecked(0));
	for (il2cpp_array_size_t i = 0; i < length; i++)
	{
		AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* currentDelegate = reinterpret_cast<AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177*>(delegatesToInvoke[i]);
		typedef void (*FunctionPointerType) (RuntimeObject*, bool, const RuntimeMethod*);
		((FunctionPointerType)currentDelegate->___invoke_impl)((Il2CppObject*)currentDelegate->___method_code, ___0_deviceWasChanged, reinterpret_cast<RuntimeMethod*>(currentDelegate->___method));
	}
}
void AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_OpenInst(AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, bool ___0_deviceWasChanged, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (bool, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_deviceWasChanged, method);
}
void AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_OpenStatic(AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, bool ___0_deviceWasChanged, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (bool, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_deviceWasChanged, method);
}
IL2CPP_EXTERN_C  void DelegatePInvokeWrapper_AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177 (AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, bool ___0_deviceWasChanged, const RuntimeMethod* method)
{
	typedef void (DEFAULT_CALL *PInvokeFunc)(int32_t);
	PInvokeFunc il2cppPInvokeFunc = reinterpret_cast<PInvokeFunc>(il2cpp_codegen_get_reverse_pinvoke_function_ptr(__this));
	il2cppPInvokeFunc(static_cast<int32_t>(___0_deviceWasChanged));

}
// Method Definition Index: 60157
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioConfigurationChangeHandler__ctor_mA9827AB9472EC8EE0A0F0FC24EBC06B4740DD944 (AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) 
{
	__this->___method_ptr = (intptr_t)il2cpp_codegen_get_method_pointer((RuntimeMethod*)___1_method);
	__this->___method = ___1_method;
	__this->___m_target = ___0_object;
	Il2CppCodeGenWriteBarrier((void**)(&__this->___m_target), (void*)___0_object);
	int parameterCount = il2cpp_codegen_method_parameter_count((RuntimeMethod*)___1_method);
	__this->___method_code = (intptr_t)__this;
	if (MethodIsStatic((RuntimeMethod*)___1_method))
	{
		bool isOpen = parameterCount == 1;
		if (isOpen)
			__this->___invoke_impl = (intptr_t)&AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_OpenStatic;
		else
			{
				__this->___invoke_impl = __this->___method_ptr;
				__this->___method_code = (intptr_t)__this->___m_target;
			}
	}
	else
	{
		if (___0_object == NULL)
			il2cpp_codegen_raise_exception(il2cpp_codegen_get_argument_exception(NULL, "Delegate to an instance method cannot have null 'this'."), NULL);
		__this->___invoke_impl = __this->___method_ptr;
		__this->___method_code = (intptr_t)__this->___m_target;
	}
	__this->___extra_arg = (intptr_t)&AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_Multicast;
}
// Method Definition Index: 60158
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2 (AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, bool ___0_deviceWasChanged, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, bool, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_deviceWasChanged, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 60159
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClip__ctor_m038DA97CB07076D1D9391E1E103F0F41D3622F89 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		__this->___m_PCMReaderCallback = (PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_PCMReaderCallback), (void*)(PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E*)NULL);
		__this->___m_PCMSetPositionCallback = (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_PCMSetPositionCallback), (void*)(PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*)NULL);
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		Object__ctor_m2149FA40CEC8D82AC20D3508AB40C0D8EFEF68E6(__this, NULL);
		return;
	}
}
// Method Definition Index: 60160
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClip_GetData_mBDEFD7D7C8E5DEA3CCEE2D7DB406DBB0C244924E (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_clip, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___1_data, int32_t ___2_numSamples, int32_t ___3_samplesOffset, const RuntimeMethod* method) 
{
	typedef bool (*AudioClip_GetData_mBDEFD7D7C8E5DEA3CCEE2D7DB406DBB0C244924E_ftn) (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*, int32_t, int32_t);
	static AudioClip_GetData_mBDEFD7D7C8E5DEA3CCEE2D7DB406DBB0C244924E_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClip_GetData_mBDEFD7D7C8E5DEA3CCEE2D7DB406DBB0C244924E_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioClip::GetData(UnityEngine.AudioClip,System.Single[],System.Int32,System.Int32)");
	bool icallRetVal = _il2cpp_icall_func(___0_clip, ___1_data, ___2_numSamples, ___3_samplesOffset);
	return icallRetVal;
}
// Method Definition Index: 60161
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClip_SetData_mB49A9BC4639C62B9C8B22319D33D46AAD176BC3B (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_clip, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___1_data, int32_t ___2_numsamples, int32_t ___3_samplesOffset, const RuntimeMethod* method) 
{
	typedef bool (*AudioClip_SetData_mB49A9BC4639C62B9C8B22319D33D46AAD176BC3B_ftn) (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*, int32_t, int32_t);
	static AudioClip_SetData_mB49A9BC4639C62B9C8B22319D33D46AAD176BC3B_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClip_SetData_mB49A9BC4639C62B9C8B22319D33D46AAD176BC3B_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioClip::SetData(UnityEngine.AudioClip,System.Single[],System.Int32,System.Int32)");
	bool icallRetVal = _il2cpp_icall_func(___0_clip, ___1_data, ___2_numsamples, ___3_samplesOffset);
	return icallRetVal;
}
// Method Definition Index: 60162
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* AudioClip_Construct_Internal_m88BC07CE3F412DDB62820F9430D1D52DA42A26F6 (const RuntimeMethod* method) 
{
	typedef AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* (*AudioClip_Construct_Internal_m88BC07CE3F412DDB62820F9430D1D52DA42A26F6_ftn) ();
	static AudioClip_Construct_Internal_m88BC07CE3F412DDB62820F9430D1D52DA42A26F6_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClip_Construct_Internal_m88BC07CE3F412DDB62820F9430D1D52DA42A26F6_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioClip::Construct_Internal()");
	AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* icallRetVal = _il2cpp_icall_func();
	return icallRetVal;
}
// Method Definition Index: 60163
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AudioClip_GetName_m561BBA037957E25D5BC5A962A1AA0C789895C9D1 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	typedef String_t* (*AudioClip_GetName_m561BBA037957E25D5BC5A962A1AA0C789895C9D1_ftn) (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*);
	static AudioClip_GetName_m561BBA037957E25D5BC5A962A1AA0C789895C9D1_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClip_GetName_m561BBA037957E25D5BC5A962A1AA0C789895C9D1_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioClip::GetName()");
	String_t* icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
// Method Definition Index: 60164
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClip_CreateUserSound_m34DA102DD6848D555D4A9D45AFAA9D3E5574BC45 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, String_t* ___0_name, int32_t ___1_lengthSamples, int32_t ___2_channels, int32_t ___3_frequency, bool ___4_stream, const RuntimeMethod* method) 
{
	typedef void (*AudioClip_CreateUserSound_m34DA102DD6848D555D4A9D45AFAA9D3E5574BC45_ftn) (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*, String_t*, int32_t, int32_t, int32_t, bool);
	static AudioClip_CreateUserSound_m34DA102DD6848D555D4A9D45AFAA9D3E5574BC45_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClip_CreateUserSound_m34DA102DD6848D555D4A9D45AFAA9D3E5574BC45_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioClip::CreateUserSound(System.String,System.Int32,System.Int32,System.Int32,System.Boolean)");
	_il2cpp_icall_func(__this, ___0_name, ___1_lengthSamples, ___2_channels, ___3_frequency, ___4_stream);
}
// Method Definition Index: 60165
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AudioClip_get_length_m6102CB29AF65988797452E4D6E43D4788303873D (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	typedef float (*AudioClip_get_length_m6102CB29AF65988797452E4D6E43D4788303873D_ftn) (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*);
	static AudioClip_get_length_m6102CB29AF65988797452E4D6E43D4788303873D_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClip_get_length_m6102CB29AF65988797452E4D6E43D4788303873D_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioClip::get_length()");
	float icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
// Method Definition Index: 60166
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AudioClip_get_samples_mDEA01CA75E7DEA0F8D480E4AF97FB96085BCF38E (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	typedef int32_t (*AudioClip_get_samples_mDEA01CA75E7DEA0F8D480E4AF97FB96085BCF38E_ftn) (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*);
	static AudioClip_get_samples_mDEA01CA75E7DEA0F8D480E4AF97FB96085BCF38E_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClip_get_samples_mDEA01CA75E7DEA0F8D480E4AF97FB96085BCF38E_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioClip::get_samples()");
	int32_t icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
// Method Definition Index: 60167
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AudioClip_get_channels_mFEECF5D6389D196BA5102EB79257298B9FDC9F84 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	typedef int32_t (*AudioClip_get_channels_mFEECF5D6389D196BA5102EB79257298B9FDC9F84_ftn) (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*);
	static AudioClip_get_channels_mFEECF5D6389D196BA5102EB79257298B9FDC9F84_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClip_get_channels_mFEECF5D6389D196BA5102EB79257298B9FDC9F84_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioClip::get_channels()");
	int32_t icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
// Method Definition Index: 60168
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AudioClip_get_frequency_m6647E10F4B2B1335187B0066E82468CCCF19647B (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	typedef int32_t (*AudioClip_get_frequency_m6647E10F4B2B1335187B0066E82468CCCF19647B_ftn) (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*);
	static AudioClip_get_frequency_m6647E10F4B2B1335187B0066E82468CCCF19647B_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClip_get_frequency_m6647E10F4B2B1335187B0066E82468CCCF19647B_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioClip::get_frequency()");
	int32_t icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
// Method Definition Index: 60169
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClip_get_isReadyToPlay_m84A0F1A9E45C8C8A6932F68B8B9124A3C55144D0 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	typedef bool (*AudioClip_get_isReadyToPlay_m84A0F1A9E45C8C8A6932F68B8B9124A3C55144D0_ftn) (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*);
	static AudioClip_get_isReadyToPlay_m84A0F1A9E45C8C8A6932F68B8B9124A3C55144D0_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClip_get_isReadyToPlay_m84A0F1A9E45C8C8A6932F68B8B9124A3C55144D0_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioClip::get_isReadyToPlay()");
	bool icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
// Method Definition Index: 60170
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AudioClip_get_loadType_m87B9E136629F7C45118EBB5B6A39273A667EE838 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	typedef int32_t (*AudioClip_get_loadType_m87B9E136629F7C45118EBB5B6A39273A667EE838_ftn) (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*);
	static AudioClip_get_loadType_m87B9E136629F7C45118EBB5B6A39273A667EE838_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClip_get_loadType_m87B9E136629F7C45118EBB5B6A39273A667EE838_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioClip::get_loadType()");
	int32_t icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
// Method Definition Index: 60171
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClip_LoadAudioData_mF43E6195AA70C39045DCF08D01C61C9DAA6876DC (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	typedef bool (*AudioClip_LoadAudioData_mF43E6195AA70C39045DCF08D01C61C9DAA6876DC_ftn) (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*);
	static AudioClip_LoadAudioData_mF43E6195AA70C39045DCF08D01C61C9DAA6876DC_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClip_LoadAudioData_mF43E6195AA70C39045DCF08D01C61C9DAA6876DC_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioClip::LoadAudioData()");
	bool icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
// Method Definition Index: 60172
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClip_UnloadAudioData_m4022A02B836CDC945D634DD7CB4DA0018F718E62 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	typedef bool (*AudioClip_UnloadAudioData_m4022A02B836CDC945D634DD7CB4DA0018F718E62_ftn) (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*);
	static AudioClip_UnloadAudioData_m4022A02B836CDC945D634DD7CB4DA0018F718E62_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClip_UnloadAudioData_m4022A02B836CDC945D634DD7CB4DA0018F718E62_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioClip::UnloadAudioData()");
	bool icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
// Method Definition Index: 60173
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClip_get_preloadAudioData_mA3D346D89D612D70EED427D95FD6CA254AE02D4A (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	typedef bool (*AudioClip_get_preloadAudioData_mA3D346D89D612D70EED427D95FD6CA254AE02D4A_ftn) (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*);
	static AudioClip_get_preloadAudioData_mA3D346D89D612D70EED427D95FD6CA254AE02D4A_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClip_get_preloadAudioData_mA3D346D89D612D70EED427D95FD6CA254AE02D4A_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioClip::get_preloadAudioData()");
	bool icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
// Method Definition Index: 60174
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClip_get_ambisonic_m56A48DCA23ABD92C967B8BD26AEC6D7CE4711304 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	typedef bool (*AudioClip_get_ambisonic_m56A48DCA23ABD92C967B8BD26AEC6D7CE4711304_ftn) (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*);
	static AudioClip_get_ambisonic_m56A48DCA23ABD92C967B8BD26AEC6D7CE4711304_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClip_get_ambisonic_m56A48DCA23ABD92C967B8BD26AEC6D7CE4711304_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioClip::get_ambisonic()");
	bool icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
// Method Definition Index: 60175
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClip_get_loadInBackground_m13F3D5F7AEA3A845D3903D93F3E986F616D64FDC (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	typedef bool (*AudioClip_get_loadInBackground_m13F3D5F7AEA3A845D3903D93F3E986F616D64FDC_ftn) (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*);
	static AudioClip_get_loadInBackground_m13F3D5F7AEA3A845D3903D93F3E986F616D64FDC_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClip_get_loadInBackground_m13F3D5F7AEA3A845D3903D93F3E986F616D64FDC_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioClip::get_loadInBackground()");
	bool icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
// Method Definition Index: 60176
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AudioClip_get_loadState_mD5E89ED3E6C1C706C021598FDF86FEB7BF5DE669 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) 
{
	typedef int32_t (*AudioClip_get_loadState_mD5E89ED3E6C1C706C021598FDF86FEB7BF5DE669_ftn) (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*);
	static AudioClip_get_loadState_mD5E89ED3E6C1C706C021598FDF86FEB7BF5DE669_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioClip_get_loadState_mD5E89ED3E6C1C706C021598FDF86FEB7BF5DE669_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioClip::get_loadState()");
	int32_t icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
// Method Definition Index: 60177
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClip_GetData_m1F6480FFDA2E354A7D8C8DE40F61AAB5AF6B4A1D (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, int32_t ___1_offsetSamples, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral8C53570A90E7EFA87FDB7F0DCC0EE1FCC019E426);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralF52B980B71426E2D226C2ED5B2045868E72F85FF);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	bool V_1 = false;
	bool V_2 = false;
	int32_t G_B5_0 = 0;
	{
		int32_t L_0;
		L_0 = AudioClip_get_channels_mFEECF5D6389D196BA5102EB79257298B9FDC9F84(__this, NULL);
		V_1 = (bool)((((int32_t)((((int32_t)L_0) > ((int32_t)0))? 1 : 0)) == ((int32_t)0))? 1 : 0);
		bool L_1 = V_1;
		if (!L_1)
		{
			goto IL_0031;
		}
	}
	{
		String_t* L_2;
		L_2 = AudioClip_GetName_m561BBA037957E25D5BC5A962A1AA0C789895C9D1(__this, NULL);
		String_t* L_3;
		L_3 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(_stringLiteralF52B980B71426E2D226C2ED5B2045868E72F85FF, L_2, _stringLiteral8C53570A90E7EFA87FDB7F0DCC0EE1FCC019E426, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_Log_m87A9A3C761FF5C43ED8A53B16190A53D08F818BB(L_3, NULL);
		V_2 = (bool)0;
		goto IL_004e;
	}

IL_0031:
	{
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_4 = ___0_data;
		if (L_4)
		{
			goto IL_0037;
		}
	}
	{
		G_B5_0 = 0;
		goto IL_0041;
	}

IL_0037:
	{
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_5 = ___0_data;
		int32_t L_6;
		L_6 = AudioClip_get_channels_mFEECF5D6389D196BA5102EB79257298B9FDC9F84(__this, NULL);
		G_B5_0 = ((int32_t)(((int32_t)(((RuntimeArray*)L_5)->max_length))/L_6));
	}

IL_0041:
	{
		V_0 = G_B5_0;
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_7 = ___0_data;
		int32_t L_8 = V_0;
		int32_t L_9 = ___1_offsetSamples;
		bool L_10;
		L_10 = AudioClip_GetData_mBDEFD7D7C8E5DEA3CCEE2D7DB406DBB0C244924E(__this, L_7, L_8, L_9, NULL);
		V_2 = L_10;
		goto IL_004e;
	}

IL_004e:
	{
		bool L_11 = V_2;
		return L_11;
	}
}
// Method Definition Index: 60178
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClip_SetData_m7B473C614C11953D746770F4F89B44600B5A6AF3 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, int32_t ___1_offsetSamples, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral5633EC5A323E76AB2DB48162D296F674B1A4AE13);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral8C53570A90E7EFA87FDB7F0DCC0EE1FCC019E426);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	bool V_1 = false;
	bool V_2 = false;
	bool V_3 = false;
	int32_t G_B5_0 = 0;
	int32_t G_B10_0 = 0;
	{
		int32_t L_0;
		L_0 = AudioClip_get_channels_mFEECF5D6389D196BA5102EB79257298B9FDC9F84(__this, NULL);
		V_0 = (bool)((((int32_t)((((int32_t)L_0) > ((int32_t)0))? 1 : 0)) == ((int32_t)0))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_0031;
		}
	}
	{
		String_t* L_2;
		L_2 = AudioClip_GetName_m561BBA037957E25D5BC5A962A1AA0C789895C9D1(__this, NULL);
		String_t* L_3;
		L_3 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(_stringLiteral5633EC5A323E76AB2DB48162D296F674B1A4AE13, L_2, _stringLiteral8C53570A90E7EFA87FDB7F0DCC0EE1FCC019E426, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_Log_m87A9A3C761FF5C43ED8A53B16190A53D08F818BB(L_3, NULL);
		V_1 = (bool)0;
		goto IL_0082;
	}

IL_0031:
	{
		int32_t L_4 = ___1_offsetSamples;
		if ((((int32_t)L_4) < ((int32_t)0)))
		{
			goto IL_0043;
		}
	}
	{
		int32_t L_5 = ___1_offsetSamples;
		int32_t L_6;
		L_6 = AudioClip_get_samples_mDEA01CA75E7DEA0F8D480E4AF97FB96085BCF38E(__this, NULL);
		G_B5_0 = ((((int32_t)((((int32_t)L_5) < ((int32_t)L_6))? 1 : 0)) == ((int32_t)0))? 1 : 0);
		goto IL_0044;
	}

IL_0043:
	{
		G_B5_0 = 1;
	}

IL_0044:
	{
		V_2 = (bool)G_B5_0;
		bool L_7 = V_2;
		if (!L_7)
		{
			goto IL_0053;
		}
	}
	{
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_8 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_8, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralA69BE8EF0F1482795ABF1CC395A765AA747ECD85)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_8, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AudioClip_SetData_m7B473C614C11953D746770F4F89B44600B5A6AF3_RuntimeMethod_var)));
	}

IL_0053:
	{
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_9 = ___0_data;
		if (!L_9)
		{
			goto IL_005d;
		}
	}
	{
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_10 = ___0_data;
		G_B10_0 = ((((int32_t)(((RuntimeArray*)L_10)->max_length)) == ((int32_t)0))? 1 : 0);
		goto IL_005e;
	}

IL_005d:
	{
		G_B10_0 = 1;
	}

IL_005e:
	{
		V_3 = (bool)G_B10_0;
		bool L_11 = V_3;
		if (!L_11)
		{
			goto IL_006d;
		}
	}
	{
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_12 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_12, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral747CA643504D60A83574C1454D344DADB4D2BA17)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_12, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AudioClip_SetData_m7B473C614C11953D746770F4F89B44600B5A6AF3_RuntimeMethod_var)));
	}

IL_006d:
	{
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_13 = ___0_data;
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_14 = ___0_data;
		int32_t L_15;
		L_15 = AudioClip_get_channels_mFEECF5D6389D196BA5102EB79257298B9FDC9F84(__this, NULL);
		int32_t L_16 = ___1_offsetSamples;
		bool L_17;
		L_17 = AudioClip_SetData_mB49A9BC4639C62B9C8B22319D33D46AAD176BC3B(__this, L_13, ((int32_t)(((int32_t)(((RuntimeArray*)L_14)->max_length))/L_15)), L_16, NULL);
		V_1 = L_17;
		goto IL_0082;
	}

IL_0082:
	{
		bool L_18 = V_1;
		return L_18;
	}
}
// Method Definition Index: 60179
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* AudioClip_Create_mF6B34084B76355CBC1991D8F4EAA878AA3A033A2 (String_t* ___0_name, int32_t ___1_lengthSamples, int32_t ___2_channels, int32_t ___3_frequency, bool ___4__3D, bool ___5_stream, const RuntimeMethod* method) 
{
	AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* V_0 = NULL;
	{
		String_t* L_0 = ___0_name;
		int32_t L_1 = ___1_lengthSamples;
		int32_t L_2 = ___2_channels;
		int32_t L_3 = ___3_frequency;
		bool L_4 = ___5_stream;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_5;
		L_5 = AudioClip_Create_mE8111F06981E42666B6A9A59D0A3EBE002D2CDFB(L_0, L_1, L_2, L_3, L_4, NULL);
		V_0 = L_5;
		goto IL_000f;
	}

IL_000f:
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_6 = V_0;
		return L_6;
	}
}
// Method Definition Index: 60180
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* AudioClip_Create_m151B4E9C35D1B185FF1605DFC93D91DFAE11DB13 (String_t* ___0_name, int32_t ___1_lengthSamples, int32_t ___2_channels, int32_t ___3_frequency, bool ___4__3D, bool ___5_stream, PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* ___6_pcmreadercallback, const RuntimeMethod* method) 
{
	AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* V_0 = NULL;
	{
		String_t* L_0 = ___0_name;
		int32_t L_1 = ___1_lengthSamples;
		int32_t L_2 = ___2_channels;
		int32_t L_3 = ___3_frequency;
		bool L_4 = ___5_stream;
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_5 = ___6_pcmreadercallback;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_6;
		L_6 = AudioClip_Create_m988FEB04BC74440E65C3CF07414E4867AAE737F8(L_0, L_1, L_2, L_3, L_4, L_5, (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*)NULL, NULL);
		V_0 = L_6;
		goto IL_0012;
	}

IL_0012:
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_7 = V_0;
		return L_7;
	}
}
// Method Definition Index: 60181
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* AudioClip_Create_m845EAF7C4DB9F316162F8441D225587CD043B9BF (String_t* ___0_name, int32_t ___1_lengthSamples, int32_t ___2_channels, int32_t ___3_frequency, bool ___4__3D, bool ___5_stream, PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* ___6_pcmreadercallback, PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* ___7_pcmsetpositioncallback, const RuntimeMethod* method) 
{
	AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* V_0 = NULL;
	{
		String_t* L_0 = ___0_name;
		int32_t L_1 = ___1_lengthSamples;
		int32_t L_2 = ___2_channels;
		int32_t L_3 = ___3_frequency;
		bool L_4 = ___5_stream;
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_5 = ___6_pcmreadercallback;
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_6 = ___7_pcmsetpositioncallback;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_7;
		L_7 = AudioClip_Create_m988FEB04BC74440E65C3CF07414E4867AAE737F8(L_0, L_1, L_2, L_3, L_4, L_5, L_6, NULL);
		V_0 = L_7;
		goto IL_0013;
	}

IL_0013:
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_8 = V_0;
		return L_8;
	}
}
// Method Definition Index: 60182
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* AudioClip_Create_mE8111F06981E42666B6A9A59D0A3EBE002D2CDFB (String_t* ___0_name, int32_t ___1_lengthSamples, int32_t ___2_channels, int32_t ___3_frequency, bool ___4_stream, const RuntimeMethod* method) 
{
	AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* V_0 = NULL;
	AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* V_1 = NULL;
	{
		String_t* L_0 = ___0_name;
		int32_t L_1 = ___1_lengthSamples;
		int32_t L_2 = ___2_channels;
		int32_t L_3 = ___3_frequency;
		bool L_4 = ___4_stream;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_5;
		L_5 = AudioClip_Create_m988FEB04BC74440E65C3CF07414E4867AAE737F8(L_0, L_1, L_2, L_3, L_4, (PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E*)NULL, (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*)NULL, NULL);
		V_0 = L_5;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_6 = V_0;
		V_1 = L_6;
		goto IL_0013;
	}

IL_0013:
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_7 = V_1;
		return L_7;
	}
}
// Method Definition Index: 60183
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* AudioClip_Create_m48B1434AA494303489CF28D8794B6CA110B51CD2 (String_t* ___0_name, int32_t ___1_lengthSamples, int32_t ___2_channels, int32_t ___3_frequency, bool ___4_stream, PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* ___5_pcmreadercallback, const RuntimeMethod* method) 
{
	AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* V_0 = NULL;
	AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* V_1 = NULL;
	{
		String_t* L_0 = ___0_name;
		int32_t L_1 = ___1_lengthSamples;
		int32_t L_2 = ___2_channels;
		int32_t L_3 = ___3_frequency;
		bool L_4 = ___4_stream;
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_5 = ___5_pcmreadercallback;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_6;
		L_6 = AudioClip_Create_m988FEB04BC74440E65C3CF07414E4867AAE737F8(L_0, L_1, L_2, L_3, L_4, L_5, (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*)NULL, NULL);
		V_0 = L_6;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_7 = V_0;
		V_1 = L_7;
		goto IL_0014;
	}

IL_0014:
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_8 = V_1;
		return L_8;
	}
}
// Method Definition Index: 60184
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* AudioClip_Create_m988FEB04BC74440E65C3CF07414E4867AAE737F8 (String_t* ___0_name, int32_t ___1_lengthSamples, int32_t ___2_channels, int32_t ___3_frequency, bool ___4_stream, PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* ___5_pcmreadercallback, PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* ___6_pcmsetpositioncallback, const RuntimeMethod* method) 
{
	AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* V_0 = NULL;
	bool V_1 = false;
	bool V_2 = false;
	bool V_3 = false;
	bool V_4 = false;
	bool V_5 = false;
	bool V_6 = false;
	AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* V_7 = NULL;
	{
		String_t* L_0 = ___0_name;
		V_1 = (bool)((((RuntimeObject*)(String_t*)L_0) == ((RuntimeObject*)(RuntimeObject*)NULL))? 1 : 0);
		bool L_1 = V_1;
		if (!L_1)
		{
			goto IL_000f;
		}
	}
	{
		NullReferenceException_tBDE63A6D24569B964908408389070C6A9F5005BB* L_2 = (NullReferenceException_tBDE63A6D24569B964908408389070C6A9F5005BB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NullReferenceException_tBDE63A6D24569B964908408389070C6A9F5005BB_il2cpp_TypeInfo_var)));
		NullReferenceException__ctor_mD26D62094A5E49C18D817817E17FDFBC1D3BD752(L_2, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_2, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AudioClip_Create_m988FEB04BC74440E65C3CF07414E4867AAE737F8_RuntimeMethod_var)));
	}

IL_000f:
	{
		int32_t L_3 = ___1_lengthSamples;
		V_2 = (bool)((((int32_t)((((int32_t)L_3) > ((int32_t)0))? 1 : 0)) == ((int32_t)0))? 1 : 0);
		bool L_4 = V_2;
		if (!L_4)
		{
			goto IL_0025;
		}
	}
	{
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_5 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_5, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralC563ED6549A22286FAAA38A5D49C5B247A52C622)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_5, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AudioClip_Create_m988FEB04BC74440E65C3CF07414E4867AAE737F8_RuntimeMethod_var)));
	}

IL_0025:
	{
		int32_t L_6 = ___2_channels;
		V_3 = (bool)((((int32_t)((((int32_t)L_6) > ((int32_t)0))? 1 : 0)) == ((int32_t)0))? 1 : 0);
		bool L_7 = V_3;
		if (!L_7)
		{
			goto IL_003b;
		}
	}
	{
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_8 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_8, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralB9ACF8E6DF73A863387F2ABAD61D84C3197C6B01)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_8, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AudioClip_Create_m988FEB04BC74440E65C3CF07414E4867AAE737F8_RuntimeMethod_var)));
	}

IL_003b:
	{
		int32_t L_9 = ___3_frequency;
		V_4 = (bool)((((int32_t)((((int32_t)L_9) > ((int32_t)0))? 1 : 0)) == ((int32_t)0))? 1 : 0);
		bool L_10 = V_4;
		if (!L_10)
		{
			goto IL_0053;
		}
	}
	{
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_11 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_11, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralCC867B94415511BD48E92E3242D7EDA425D20035)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_11, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AudioClip_Create_m988FEB04BC74440E65C3CF07414E4867AAE737F8_RuntimeMethod_var)));
	}

IL_0053:
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_12;
		L_12 = AudioClip_Construct_Internal_m88BC07CE3F412DDB62820F9430D1D52DA42A26F6(NULL);
		V_0 = L_12;
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_13 = ___5_pcmreadercallback;
		V_5 = (bool)((!(((RuntimeObject*)(PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E*)L_13) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		bool L_14 = V_5;
		if (!L_14)
		{
			goto IL_006d;
		}
	}
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_15 = V_0;
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_16 = ___5_pcmreadercallback;
		AudioClip_add_m_PCMReaderCallback_mA226EA143D90E04117A740FC9FA9F1111346CA83(L_15, L_16, NULL);
	}

IL_006d:
	{
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_17 = ___6_pcmsetpositioncallback;
		V_6 = (bool)((!(((RuntimeObject*)(PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*)L_17) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		bool L_18 = V_6;
		if (!L_18)
		{
			goto IL_0081;
		}
	}
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_19 = V_0;
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_20 = ___6_pcmsetpositioncallback;
		AudioClip_add_m_PCMSetPositionCallback_mB280AD93A847C65F536D846FECC7DCBE9266C37F(L_19, L_20, NULL);
	}

IL_0081:
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_21 = V_0;
		String_t* L_22 = ___0_name;
		int32_t L_23 = ___1_lengthSamples;
		int32_t L_24 = ___2_channels;
		int32_t L_25 = ___3_frequency;
		bool L_26 = ___4_stream;
		AudioClip_CreateUserSound_m34DA102DD6848D555D4A9D45AFAA9D3E5574BC45(L_21, L_22, L_23, L_24, L_25, L_26, NULL);
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_27 = V_0;
		V_7 = L_27;
		goto IL_0093;
	}

IL_0093:
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_28 = V_7;
		return L_28;
	}
}
// Method Definition Index: 60185
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClip_add_m_PCMReaderCallback_mA226EA143D90E04117A740FC9FA9F1111346CA83 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* V_0 = NULL;
	PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* V_1 = NULL;
	PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* V_2 = NULL;
	{
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_0 = __this->___m_PCMReaderCallback;
		V_0 = L_0;
	}

IL_0007:
	{
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_1 = V_0;
		V_1 = L_1;
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_2 = V_1;
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_3 = ___0_value;
		Delegate_t* L_4;
		L_4 = Delegate_Combine_m1F725AEF318BE6F0426863490691A6F4606E7D00(L_2, L_3, NULL);
		V_2 = ((PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E*)CastclassSealed((RuntimeObject*)L_4, PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E_il2cpp_TypeInfo_var));
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E** L_5 = (PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E**)(&__this->___m_PCMReaderCallback);
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_6 = V_2;
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_7 = V_1;
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_8;
		L_8 = InterlockedCompareExchangeImpl<PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E*>(L_5, L_6, L_7);
		V_0 = L_8;
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_9 = V_0;
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_10 = V_1;
		if ((!(((RuntimeObject*)(PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E*)L_9) == ((RuntimeObject*)(PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E*)L_10))))
		{
			goto IL_0007;
		}
	}
	{
		return;
	}
}
// Method Definition Index: 60186
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClip_remove_m_PCMReaderCallback_m3258A455005F4A94570B4F8FE28A5EDA91A88412 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* V_0 = NULL;
	PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* V_1 = NULL;
	PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* V_2 = NULL;
	{
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_0 = __this->___m_PCMReaderCallback;
		V_0 = L_0;
	}

IL_0007:
	{
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_1 = V_0;
		V_1 = L_1;
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_2 = V_1;
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_3 = ___0_value;
		Delegate_t* L_4;
		L_4 = Delegate_Remove_m8B7DD5661308FA972E23CA1CC3FC9CEB355504E3(L_2, L_3, NULL);
		V_2 = ((PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E*)CastclassSealed((RuntimeObject*)L_4, PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E_il2cpp_TypeInfo_var));
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E** L_5 = (PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E**)(&__this->___m_PCMReaderCallback);
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_6 = V_2;
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_7 = V_1;
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_8;
		L_8 = InterlockedCompareExchangeImpl<PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E*>(L_5, L_6, L_7);
		V_0 = L_8;
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_9 = V_0;
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_10 = V_1;
		if ((!(((RuntimeObject*)(PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E*)L_9) == ((RuntimeObject*)(PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E*)L_10))))
		{
			goto IL_0007;
		}
	}
	{
		return;
	}
}
// Method Definition Index: 60187
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClip_add_m_PCMSetPositionCallback_mB280AD93A847C65F536D846FECC7DCBE9266C37F (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* V_0 = NULL;
	PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* V_1 = NULL;
	PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* V_2 = NULL;
	{
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_0 = __this->___m_PCMSetPositionCallback;
		V_0 = L_0;
	}

IL_0007:
	{
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_1 = V_0;
		V_1 = L_1;
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_2 = V_1;
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_3 = ___0_value;
		Delegate_t* L_4;
		L_4 = Delegate_Combine_m1F725AEF318BE6F0426863490691A6F4606E7D00(L_2, L_3, NULL);
		V_2 = ((PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*)CastclassSealed((RuntimeObject*)L_4, PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072_il2cpp_TypeInfo_var));
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072** L_5 = (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072**)(&__this->___m_PCMSetPositionCallback);
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_6 = V_2;
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_7 = V_1;
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_8;
		L_8 = InterlockedCompareExchangeImpl<PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*>(L_5, L_6, L_7);
		V_0 = L_8;
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_9 = V_0;
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_10 = V_1;
		if ((!(((RuntimeObject*)(PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*)L_9) == ((RuntimeObject*)(PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*)L_10))))
		{
			goto IL_0007;
		}
	}
	{
		return;
	}
}
// Method Definition Index: 60188
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClip_remove_m_PCMSetPositionCallback_m39598139640580138742F129E0510917DF2E233C (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* V_0 = NULL;
	PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* V_1 = NULL;
	PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* V_2 = NULL;
	{
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_0 = __this->___m_PCMSetPositionCallback;
		V_0 = L_0;
	}

IL_0007:
	{
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_1 = V_0;
		V_1 = L_1;
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_2 = V_1;
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_3 = ___0_value;
		Delegate_t* L_4;
		L_4 = Delegate_Remove_m8B7DD5661308FA972E23CA1CC3FC9CEB355504E3(L_2, L_3, NULL);
		V_2 = ((PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*)CastclassSealed((RuntimeObject*)L_4, PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072_il2cpp_TypeInfo_var));
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072** L_5 = (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072**)(&__this->___m_PCMSetPositionCallback);
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_6 = V_2;
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_7 = V_1;
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_8;
		L_8 = InterlockedCompareExchangeImpl<PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*>(L_5, L_6, L_7);
		V_0 = L_8;
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_9 = V_0;
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_10 = V_1;
		if ((!(((RuntimeObject*)(PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*)L_9) == ((RuntimeObject*)(PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*)L_10))))
		{
			goto IL_0007;
		}
	}
	{
		return;
	}
}
// Method Definition Index: 60189
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClip_InvokePCMReaderCallback_Internal_m766E5705AB5AE16F5F142867CC3758ABE4BF462C (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_0 = __this->___m_PCMReaderCallback;
		V_0 = (bool)((!(((RuntimeObject*)(PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E*)L_0) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_001b;
		}
	}
	{
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* L_2 = __this->___m_PCMReaderCallback;
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_3 = ___0_data;
		PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_inline(L_2, L_3, NULL);
	}

IL_001b:
	{
		return;
	}
}
// Method Definition Index: 60190
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioClip_InvokePCMSetPositionCallback_Internal_m986EF703B7DDE42343730DE93A095D05B9F4DBB8 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, int32_t ___0_position, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_0 = __this->___m_PCMSetPositionCallback;
		V_0 = (bool)((!(((RuntimeObject*)(PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*)L_0) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_001b;
		}
	}
	{
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* L_2 = __this->___m_PCMSetPositionCallback;
		int32_t L_3 = ___0_position;
		PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_inline(L_2, L_3, NULL);
	}

IL_001b:
	{
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
void PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_Multicast(PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method)
{
	il2cpp_array_size_t length = __this->___delegates->max_length;
	Delegate_t** delegatesToInvoke = reinterpret_cast<Delegate_t**>(__this->___delegates->GetAddressAtUnchecked(0));
	for (il2cpp_array_size_t i = 0; i < length; i++)
	{
		PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* currentDelegate = reinterpret_cast<PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E*>(delegatesToInvoke[i]);
		typedef void (*FunctionPointerType) (RuntimeObject*, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*, const RuntimeMethod*);
		((FunctionPointerType)currentDelegate->___invoke_impl)((Il2CppObject*)currentDelegate->___method_code, ___0_data, reinterpret_cast<RuntimeMethod*>(currentDelegate->___method));
	}
}
void PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_OpenInst(PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_data, method);
}
void PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_OpenStatic(PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_data, method);
}
IL2CPP_EXTERN_C  void DelegatePInvokeWrapper_PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E (PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method)
{
	typedef void (DEFAULT_CALL *PInvokeFunc)(float*);
	PInvokeFunc il2cppPInvokeFunc = reinterpret_cast<PInvokeFunc>(il2cpp_codegen_get_reverse_pinvoke_function_ptr(__this));
	float* ____0_data_marshaled = NULL;
	if (___0_data != NULL)
	{
		____0_data_marshaled = reinterpret_cast<float*>((___0_data)->GetAddressAtUnchecked(0));
	}

	il2cppPInvokeFunc(____0_data_marshaled);

}
// Method Definition Index: 60191
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PCMReaderCallback__ctor_mF621B6CC1A4BA6525190C5037401CF2FD5C0CF28 (PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) 
{
	__this->___method_ptr = (intptr_t)il2cpp_codegen_get_method_pointer((RuntimeMethod*)___1_method);
	__this->___method = ___1_method;
	__this->___m_target = ___0_object;
	Il2CppCodeGenWriteBarrier((void**)(&__this->___m_target), (void*)___0_object);
	int parameterCount = il2cpp_codegen_method_parameter_count((RuntimeMethod*)___1_method);
	__this->___method_code = (intptr_t)__this;
	if (MethodIsStatic((RuntimeMethod*)___1_method))
	{
		bool isOpen = parameterCount == 1;
		if (isOpen)
			__this->___invoke_impl = (intptr_t)&PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_OpenStatic;
		else
			{
				__this->___invoke_impl = __this->___method_ptr;
				__this->___method_code = (intptr_t)__this->___m_target;
			}
	}
	else
	{
		bool isOpen = parameterCount == 0;
		if (isOpen)
		{
			__this->___invoke_impl = (intptr_t)&PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_OpenInst;
		}
		else
		{
			if (___0_object == NULL)
				il2cpp_codegen_raise_exception(il2cpp_codegen_get_argument_exception(NULL, "Delegate to an instance method cannot have null 'this'."), NULL);
			__this->___invoke_impl = __this->___method_ptr;
			__this->___method_code = (intptr_t)__this->___m_target;
		}
	}
	__this->___extra_arg = (intptr_t)&PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_Multicast;
}
// Method Definition Index: 60192
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152 (PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_data, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
void PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_Multicast(PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, int32_t ___0_position, const RuntimeMethod* method)
{
	il2cpp_array_size_t length = __this->___delegates->max_length;
	Delegate_t** delegatesToInvoke = reinterpret_cast<Delegate_t**>(__this->___delegates->GetAddressAtUnchecked(0));
	for (il2cpp_array_size_t i = 0; i < length; i++)
	{
		PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* currentDelegate = reinterpret_cast<PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072*>(delegatesToInvoke[i]);
		typedef void (*FunctionPointerType) (RuntimeObject*, int32_t, const RuntimeMethod*);
		((FunctionPointerType)currentDelegate->___invoke_impl)((Il2CppObject*)currentDelegate->___method_code, ___0_position, reinterpret_cast<RuntimeMethod*>(currentDelegate->___method));
	}
}
void PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_OpenInst(PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, int32_t ___0_position, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (int32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_position, method);
}
void PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_OpenStatic(PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, int32_t ___0_position, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (int32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_position, method);
}
IL2CPP_EXTERN_C  void DelegatePInvokeWrapper_PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072 (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, int32_t ___0_position, const RuntimeMethod* method)
{
	typedef void (DEFAULT_CALL *PInvokeFunc)(int32_t);
	PInvokeFunc il2cppPInvokeFunc = reinterpret_cast<PInvokeFunc>(il2cpp_codegen_get_reverse_pinvoke_function_ptr(__this));
	il2cppPInvokeFunc(___0_position);

}
// Method Definition Index: 60193
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PCMSetPositionCallback__ctor_mD16F77DDB552EB69BB3F5EF39420B2F09F95455B (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) 
{
	__this->___method_ptr = (intptr_t)il2cpp_codegen_get_method_pointer((RuntimeMethod*)___1_method);
	__this->___method = ___1_method;
	__this->___m_target = ___0_object;
	Il2CppCodeGenWriteBarrier((void**)(&__this->___m_target), (void*)___0_object);
	int parameterCount = il2cpp_codegen_method_parameter_count((RuntimeMethod*)___1_method);
	__this->___method_code = (intptr_t)__this;
	if (MethodIsStatic((RuntimeMethod*)___1_method))
	{
		bool isOpen = parameterCount == 1;
		if (isOpen)
			__this->___invoke_impl = (intptr_t)&PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_OpenStatic;
		else
			{
				__this->___invoke_impl = __this->___method_ptr;
				__this->___method_code = (intptr_t)__this->___m_target;
			}
	}
	else
	{
		if (___0_object == NULL)
			il2cpp_codegen_raise_exception(il2cpp_codegen_get_argument_exception(NULL, "Delegate to an instance method cannot have null 'this'."), NULL);
		__this->___invoke_impl = __this->___method_ptr;
		__this->___method_code = (intptr_t)__this->___m_target;
	}
	__this->___extra_arg = (intptr_t)&PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_Multicast;
}
// Method Definition Index: 60194
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702 (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, int32_t ___0_position, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, int32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_position, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 60195
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AudioListener_get_volume_m8EAB8FBA127A53E689C1D8C1857781070381974A (const RuntimeMethod* method) 
{
	typedef float (*AudioListener_get_volume_m8EAB8FBA127A53E689C1D8C1857781070381974A_ftn) ();
	static AudioListener_get_volume_m8EAB8FBA127A53E689C1D8C1857781070381974A_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioListener_get_volume_m8EAB8FBA127A53E689C1D8C1857781070381974A_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioListener::get_volume()");
	float icallRetVal = _il2cpp_icall_func();
	return icallRetVal;
}
// Method Definition Index: 60196
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioListener_set_volume_m72BAF2D558A5449091A59630EBF48095DEB4C721 (float ___0_value, const RuntimeMethod* method) 
{
	typedef void (*AudioListener_set_volume_m72BAF2D558A5449091A59630EBF48095DEB4C721_ftn) (float);
	static AudioListener_set_volume_m72BAF2D558A5449091A59630EBF48095DEB4C721_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioListener_set_volume_m72BAF2D558A5449091A59630EBF48095DEB4C721_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioListener::set_volume(System.Single)");
	_il2cpp_icall_func(___0_value);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 60197
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AudioSource_GetPitch_m80F6D2BAF966F669253E9231AFCFFC303779913D (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_source, const RuntimeMethod* method) 
{
	typedef float (*AudioSource_GetPitch_m80F6D2BAF966F669253E9231AFCFFC303779913D_ftn) (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299*);
	static AudioSource_GetPitch_m80F6D2BAF966F669253E9231AFCFFC303779913D_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_GetPitch_m80F6D2BAF966F669253E9231AFCFFC303779913D_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::GetPitch(UnityEngine.AudioSource)");
	float icallRetVal = _il2cpp_icall_func(___0_source);
	return icallRetVal;
}
// Method Definition Index: 60198
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_SetPitch_mE75DEDF8F37301BDA63E0F545A7A00850C24F53E (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_source, float ___1_pitch, const RuntimeMethod* method) 
{
	typedef void (*AudioSource_SetPitch_mE75DEDF8F37301BDA63E0F545A7A00850C24F53E_ftn) (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299*, float);
	static AudioSource_SetPitch_mE75DEDF8F37301BDA63E0F545A7A00850C24F53E_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_SetPitch_mE75DEDF8F37301BDA63E0F545A7A00850C24F53E_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::SetPitch(UnityEngine.AudioSource,System.Single)");
	_il2cpp_icall_func(___0_source, ___1_pitch);
}
// Method Definition Index: 60199
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayHelper_m4DE8C48925C3548BED306DAB9F87939F24A46960 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_source, uint64_t ___1_delay, const RuntimeMethod* method) 
{
	typedef void (*AudioSource_PlayHelper_m4DE8C48925C3548BED306DAB9F87939F24A46960_ftn) (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299*, uint64_t);
	static AudioSource_PlayHelper_m4DE8C48925C3548BED306DAB9F87939F24A46960_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_PlayHelper_m4DE8C48925C3548BED306DAB9F87939F24A46960_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::PlayHelper(UnityEngine.AudioSource,System.UInt64)");
	_il2cpp_icall_func(___0_source, ___1_delay);
}
// Method Definition Index: 60200
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_Play_m10DB5ACD1CC32EE433DBC10416B1450A30DE5F16 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, double ___0_delay, const RuntimeMethod* method) 
{
	typedef void (*AudioSource_Play_m10DB5ACD1CC32EE433DBC10416B1450A30DE5F16_ftn) (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299*, double);
	static AudioSource_Play_m10DB5ACD1CC32EE433DBC10416B1450A30DE5F16_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_Play_m10DB5ACD1CC32EE433DBC10416B1450A30DE5F16_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::Play(System.Double)");
	_il2cpp_icall_func(__this, ___0_delay);
}
// Method Definition Index: 60201
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayOneShotHelper_mD110EAF42353687BD0B1190EEF30F0C65A4CF265 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___0_source, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___1_clip, float ___2_volumeScale, const RuntimeMethod* method) 
{
	typedef void (*AudioSource_PlayOneShotHelper_mD110EAF42353687BD0B1190EEF30F0C65A4CF265_ftn) (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299*, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*, float);
	static AudioSource_PlayOneShotHelper_mD110EAF42353687BD0B1190EEF30F0C65A4CF265_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_PlayOneShotHelper_mD110EAF42353687BD0B1190EEF30F0C65A4CF265_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::PlayOneShotHelper(UnityEngine.AudioSource,UnityEngine.AudioClip,System.Single)");
	_il2cpp_icall_func(___0_source, ___1_clip, ___2_volumeScale);
}
// Method Definition Index: 60202
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AudioSource_get_volume_m9CCF33BC636562EA282FDE07463B547D70134EE3 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) 
{
	typedef float (*AudioSource_get_volume_m9CCF33BC636562EA282FDE07463B547D70134EE3_ftn) (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299*);
	static AudioSource_get_volume_m9CCF33BC636562EA282FDE07463B547D70134EE3_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_get_volume_m9CCF33BC636562EA282FDE07463B547D70134EE3_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::get_volume()");
	float icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
// Method Definition Index: 60203
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_volume_mD902BBDBBDE0E3C148609BF3C05096148E90F2C0 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, float ___0_value, const RuntimeMethod* method) 
{
	typedef void (*AudioSource_set_volume_mD902BBDBBDE0E3C148609BF3C05096148E90F2C0_ftn) (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299*, float);
	static AudioSource_set_volume_mD902BBDBBDE0E3C148609BF3C05096148E90F2C0_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_set_volume_mD902BBDBBDE0E3C148609BF3C05096148E90F2C0_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::set_volume(System.Single)");
	_il2cpp_icall_func(__this, ___0_value);
}
// Method Definition Index: 60204
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AudioSource_get_pitch_mB1B0B8A52400B5C798BF1E644FE1C2FFA20A9863 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	{
		float L_0;
		L_0 = AudioSource_GetPitch_m80F6D2BAF966F669253E9231AFCFFC303779913D(__this, NULL);
		V_0 = L_0;
		goto IL_000a;
	}

IL_000a:
	{
		float L_1 = V_0;
		return L_1;
	}
}
// Method Definition Index: 60205
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_pitch_mD14631FC99BF38AAFB356D9C45546BC16CF9E811 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, float ___0_value, const RuntimeMethod* method) 
{
	{
		float L_0 = ___0_value;
		AudioSource_SetPitch_mE75DEDF8F37301BDA63E0F545A7A00850C24F53E(__this, L_0, NULL);
		return;
	}
}
// Method Definition Index: 60206
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* AudioSource_get_clip_m4F5027066F9FC44B44192713142B0C277BB418FE (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) 
{
	typedef AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* (*AudioSource_get_clip_m4F5027066F9FC44B44192713142B0C277BB418FE_ftn) (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299*);
	static AudioSource_get_clip_m4F5027066F9FC44B44192713142B0C277BB418FE_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_get_clip_m4F5027066F9FC44B44192713142B0C277BB418FE_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::get_clip()");
	AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
// Method Definition Index: 60207
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_clip_mFF441895E274286C88D9C75ED5CA1B1B39528D70 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_value, const RuntimeMethod* method) 
{
	typedef void (*AudioSource_set_clip_mFF441895E274286C88D9C75ED5CA1B1B39528D70_ftn) (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299*, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*);
	static AudioSource_set_clip_mFF441895E274286C88D9C75ED5CA1B1B39528D70_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_set_clip_mFF441895E274286C88D9C75ED5CA1B1B39528D70_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::set_clip(UnityEngine.AudioClip)");
	_il2cpp_icall_func(__this, ___0_value);
}
// Method Definition Index: 60208
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_Play_m95DF07111C61D0E0F00257A00384D31531D590C3 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) 
{
	{
		AudioSource_PlayHelper_m4DE8C48925C3548BED306DAB9F87939F24A46960(__this, ((int64_t)0), NULL);
		return;
	}
}
// Method Definition Index: 60209
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayDelayed_m6A4992F1A010DC12906C6002B22F19082967770E (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, float ___0_delay, const RuntimeMethod* method) 
{
	AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* G_B2_0 = NULL;
	AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* G_B1_0 = NULL;
	double G_B3_0 = 0.0;
	AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* G_B3_1 = NULL;
	{
		float L_0 = ___0_delay;
		if ((((float)L_0) < ((float)(0.0f))))
		{
			G_B2_0 = __this;
			goto IL_000f;
		}
		G_B1_0 = __this;
	}
	{
		float L_1 = ___0_delay;
		G_B3_0 = ((-((double)L_1)));
		G_B3_1 = G_B1_0;
		goto IL_0018;
	}

IL_000f:
	{
		G_B3_0 = (0.0);
		G_B3_1 = G_B2_0;
	}

IL_0018:
	{
		AudioSource_Play_m10DB5ACD1CC32EE433DBC10416B1450A30DE5F16(G_B3_1, G_B3_0, NULL);
		return;
	}
}
// Method Definition Index: 60210
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_PlayOneShot_mF6FE95C58996B38EF6E7F7482F95F5E15E0AB30B (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_clip, float ___1_volumeScale, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral49403A17E8D32B35CB5B66AB1A2651A7EEAD1B00);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_0 = ___0_clip;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		V_0 = L_1;
		bool L_2 = V_0;
		if (!L_2)
		{
			goto IL_001a;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteral49403A17E8D32B35CB5B66AB1A2651A7EEAD1B00, NULL);
		goto IL_0023;
	}

IL_001a:
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_3 = ___0_clip;
		float L_4 = ___1_volumeScale;
		AudioSource_PlayOneShotHelper_mD110EAF42353687BD0B1190EEF30F0C65A4CF265(__this, L_3, L_4, NULL);
	}

IL_0023:
	{
		return;
	}
}
// Method Definition Index: 60211
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioSource_get_isPlaying_mC203303F2F7146B2C056CB47B9391463FDF408FC (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) 
{
	typedef bool (*AudioSource_get_isPlaying_mC203303F2F7146B2C056CB47B9391463FDF408FC_ftn) (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299*);
	static AudioSource_get_isPlaying_mC203303F2F7146B2C056CB47B9391463FDF408FC_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_get_isPlaying_mC203303F2F7146B2C056CB47B9391463FDF408FC_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::get_isPlaying()");
	bool icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
// Method Definition Index: 60212
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioSource_get_loop_m2D83BF58E1BD1BEE4CC80413C12E761D3310FC2C (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) 
{
	typedef bool (*AudioSource_get_loop_m2D83BF58E1BD1BEE4CC80413C12E761D3310FC2C_ftn) (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299*);
	static AudioSource_get_loop_m2D83BF58E1BD1BEE4CC80413C12E761D3310FC2C_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioSource_get_loop_m2D83BF58E1BD1BEE4CC80413C12E761D3310FC2C_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AudioSource::get_loop()");
	bool icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 60213
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSampleProvider_InvokeSampleFramesAvailable_mEB16F7230AB65A3576BF053AC5719F8E134FBCD4 (AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* __this, int32_t ___0_sampleFrameCount, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* L_0 = __this->___sampleFramesAvailable;
		V_0 = (bool)((!(((RuntimeObject*)(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30*)L_0) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_001c;
		}
	}
	{
		SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* L_2 = __this->___sampleFramesAvailable;
		int32_t L_3 = ___0_sampleFrameCount;
		SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_inline(L_2, __this, L_3, NULL);
	}

IL_001c:
	{
		return;
	}
}
// Method Definition Index: 60214
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSampleProvider_InvokeSampleFramesOverflow_m66593173A527981F5EB2A5EF77B0C9119DAB5E15 (AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* __this, int32_t ___0_droppedSampleFrameCount, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* L_0 = __this->___sampleFramesOverflow;
		V_0 = (bool)((!(((RuntimeObject*)(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30*)L_0) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_001c;
		}
	}
	{
		SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* L_2 = __this->___sampleFramesOverflow;
		int32_t L_3 = ___0_droppedSampleFrameCount;
		SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_inline(L_2, __this, L_3, NULL);
	}

IL_001c:
	{
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_Multicast(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method)
{
	il2cpp_array_size_t length = __this->___delegates->max_length;
	Delegate_t** delegatesToInvoke = reinterpret_cast<Delegate_t**>(__this->___delegates->GetAddressAtUnchecked(0));
	for (il2cpp_array_size_t i = 0; i < length; i++)
	{
		SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* currentDelegate = reinterpret_cast<SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30*>(delegatesToInvoke[i]);
		typedef void (*FunctionPointerType) (RuntimeObject*, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2*, uint32_t, const RuntimeMethod*);
		((FunctionPointerType)currentDelegate->___invoke_impl)((Il2CppObject*)currentDelegate->___method_code, ___0_provider, ___1_sampleFrameCount, reinterpret_cast<RuntimeMethod*>(currentDelegate->___method));
	}
}
void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenInst(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2*, uint32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_provider, ___1_sampleFrameCount, method);
}
void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenStatic(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2*, uint32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr)(___0_provider, ___1_sampleFrameCount, method);
}
void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenVirtual(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method)
{
	VirtualActionInvoker1< uint32_t >::Invoke(il2cpp_codegen_method_get_slot(method), ___0_provider, ___1_sampleFrameCount);
}
void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenInterface(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method)
{
	InterfaceActionInvoker1< uint32_t >::Invoke(il2cpp_codegen_method_get_slot(method), il2cpp_codegen_method_get_declaring_type(method), ___0_provider, ___1_sampleFrameCount);
}
void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenGenericVirtual(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method)
{
	GenericVirtualActionInvoker1< uint32_t >::Invoke(method, ___0_provider, ___1_sampleFrameCount);
}
void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenGenericInterface(SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method)
{
	GenericInterfaceActionInvoker1< uint32_t >::Invoke(method, ___0_provider, ___1_sampleFrameCount);
}
// Method Definition Index: 60215
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SampleFramesHandler__ctor_m7DDE0BAD439CD80791140C7D42D661B598A7663A (SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) 
{
	__this->___method_ptr = (intptr_t)il2cpp_codegen_get_method_pointer((RuntimeMethod*)___1_method);
	__this->___method = ___1_method;
	__this->___m_target = ___0_object;
	Il2CppCodeGenWriteBarrier((void**)(&__this->___m_target), (void*)___0_object);
	int parameterCount = il2cpp_codegen_method_parameter_count((RuntimeMethod*)___1_method);
	__this->___method_code = (intptr_t)__this;
	if (MethodIsStatic((RuntimeMethod*)___1_method))
	{
		bool isOpen = parameterCount == 2;
		if (isOpen)
			__this->___invoke_impl = (intptr_t)&SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenStatic;
		else
			{
				__this->___invoke_impl = __this->___method_ptr;
				__this->___method_code = (intptr_t)__this->___m_target;
			}
	}
	else
	{
		bool isOpen = parameterCount == 1;
		if (isOpen)
		{
			if (__this->___method_is_virtual)
			{
				if (il2cpp_codegen_method_is_generic_instance_method((RuntimeMethod*)___1_method))
					if (il2cpp_codegen_method_is_interface_method((RuntimeMethod*)___1_method))
						__this->___invoke_impl = (intptr_t)&SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenGenericInterface;
					else
						__this->___invoke_impl = (intptr_t)&SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenGenericVirtual;
				else
					if (il2cpp_codegen_method_is_interface_method((RuntimeMethod*)___1_method))
						__this->___invoke_impl = (intptr_t)&SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenInterface;
					else
						__this->___invoke_impl = (intptr_t)&SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenVirtual;
			}
			else
			{
				__this->___invoke_impl = (intptr_t)&SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_OpenInst;
			}
		}
		else
		{
			if (___0_object == NULL)
				il2cpp_codegen_raise_exception(il2cpp_codegen_get_argument_exception(NULL, "Delegate to an instance method cannot have null 'this'."), NULL);
			__this->___invoke_impl = __this->___method_ptr;
			__this->___method_code = (intptr_t)__this->___m_target;
		}
	}
	__this->___extra_arg = (intptr_t)&SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_Multicast;
}
// Method Definition Index: 60216
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC (SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2*, uint32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_provider, ___1_sampleFrameCount, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 60217
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 AudioClipPlayable_GetHandle_mEA1D664328FF9B08E4F7D5EBCD4B51A754D97C44 (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* __this, const RuntimeMethod* method) 
{
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_0 = __this->___m_Handle;
		V_0 = L_0;
		goto IL_000a;
	}

IL_000a:
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_1 = V_0;
		return L_1;
	}
}
IL2CPP_EXTERN_C  PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 AudioClipPlayable_GetHandle_mEA1D664328FF9B08E4F7D5EBCD4B51A754D97C44_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0*>(__this + _offset);
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 _returnValue;
	_returnValue = AudioClipPlayable_GetHandle_mEA1D664328FF9B08E4F7D5EBCD4B51A754D97C44(_thisAdjusted, method);
	return _returnValue;
}
// Method Definition Index: 60218
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioClipPlayable_Equals_m9C1C75ACBB74FE06AD02BE4643F6EB39413EFF83 (AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* __this, AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 ___0_other, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_0;
		L_0 = AudioClipPlayable_GetHandle_mEA1D664328FF9B08E4F7D5EBCD4B51A754D97C44(__this, NULL);
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_1;
		L_1 = AudioClipPlayable_GetHandle_mEA1D664328FF9B08E4F7D5EBCD4B51A754D97C44((&___0_other), NULL);
		il2cpp_codegen_runtime_class_init_inline(PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		bool L_2;
		L_2 = PlayableHandle_op_Equality_m0E6C48A28F75A870AC22ADE3BD42F7F70A43C99C(L_0, L_1, NULL);
		V_0 = L_2;
		goto IL_0016;
	}

IL_0016:
	{
		bool L_3 = V_0;
		return L_3;
	}
}
IL2CPP_EXTERN_C  bool AudioClipPlayable_Equals_m9C1C75ACBB74FE06AD02BE4643F6EB39413EFF83_AdjustorThunk (RuntimeObject* __this, AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0 ___0_other, const RuntimeMethod* method)
{
	AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AudioClipPlayable_tD4B758E68CAE03CB0CD31F90C8A3E603B97143A0*>(__this + _offset);
	bool _returnValue;
	_returnValue = AudioClipPlayable_Equals_m9C1C75ACBB74FE06AD02BE4643F6EB39413EFF83(_thisAdjusted, ___0_other, method);
	return _returnValue;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 60219
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioMixer__ctor_m8BB9BFC96DB436EE4CECE0BECECD5DFC7559058D (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		Object__ctor_m2149FA40CEC8D82AC20D3508AB40C0D8EFEF68E6(__this, NULL);
		return;
	}
}
// Method Definition Index: 60220
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311* AudioMixer_get_outputAudioMixerGroup_m0B5B993AB7FD678B15276E06B226B06B709C560C (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* __this, const RuntimeMethod* method) 
{
	typedef AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311* (*AudioMixer_get_outputAudioMixerGroup_m0B5B993AB7FD678B15276E06B226B06B709C560C_ftn) (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04*);
	static AudioMixer_get_outputAudioMixerGroup_m0B5B993AB7FD678B15276E06B226B06B709C560C_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioMixer_get_outputAudioMixerGroup_m0B5B993AB7FD678B15276E06B226B06B709C560C_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioMixer::get_outputAudioMixerGroup()");
	AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311* icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
// Method Definition Index: 60221
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioMixer_set_outputAudioMixerGroup_m7362B6469DCAFB7D1A65BC20BDCF42AE76F90306 (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* __this, AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311* ___0_value, const RuntimeMethod* method) 
{
	typedef void (*AudioMixer_set_outputAudioMixerGroup_m7362B6469DCAFB7D1A65BC20BDCF42AE76F90306_ftn) (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04*, AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311*);
	static AudioMixer_set_outputAudioMixerGroup_m7362B6469DCAFB7D1A65BC20BDCF42AE76F90306_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioMixer_set_outputAudioMixerGroup_m7362B6469DCAFB7D1A65BC20BDCF42AE76F90306_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioMixer::set_outputAudioMixerGroup(UnityEngine.Audio.AudioMixerGroup)");
	_il2cpp_icall_func(__this, ___0_value);
}
// Method Definition Index: 60222
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781* AudioMixer_FindSnapshot_m289C3F55A58E9DE4EEE456AEFE444ECA4D3496C5 (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* __this, String_t* ___0_name, const RuntimeMethod* method) 
{
	typedef AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781* (*AudioMixer_FindSnapshot_m289C3F55A58E9DE4EEE456AEFE444ECA4D3496C5_ftn) (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04*, String_t*);
	static AudioMixer_FindSnapshot_m289C3F55A58E9DE4EEE456AEFE444ECA4D3496C5_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioMixer_FindSnapshot_m289C3F55A58E9DE4EEE456AEFE444ECA4D3496C5_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioMixer::FindSnapshot(System.String)");
	AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781* icallRetVal = _il2cpp_icall_func(__this, ___0_name);
	return icallRetVal;
}
// Method Definition Index: 60223
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioMixerGroupU5BU5D_t8FDE37A00A7E874EB75184A6133A160137D0532B* AudioMixer_FindMatchingGroups_m4541BE177FFA0225AF159156ABB5FE3F5F6CF2CF (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* __this, String_t* ___0_subPath, const RuntimeMethod* method) 
{
	typedef AudioMixerGroupU5BU5D_t8FDE37A00A7E874EB75184A6133A160137D0532B* (*AudioMixer_FindMatchingGroups_m4541BE177FFA0225AF159156ABB5FE3F5F6CF2CF_ftn) (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04*, String_t*);
	static AudioMixer_FindMatchingGroups_m4541BE177FFA0225AF159156ABB5FE3F5F6CF2CF_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioMixer_FindMatchingGroups_m4541BE177FFA0225AF159156ABB5FE3F5F6CF2CF_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioMixer::FindMatchingGroups(System.String)");
	AudioMixerGroupU5BU5D_t8FDE37A00A7E874EB75184A6133A160137D0532B* icallRetVal = _il2cpp_icall_func(__this, ___0_subPath);
	return icallRetVal;
}
// Method Definition Index: 60224
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioMixer_TransitionToSnapshot_m308E8D50A4573EFDD381ED7DEC23162F8CD0EB5D (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* __this, AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781* ___0_snapshot, float ___1_timeToReach, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	bool V_1 = false;
	{
		AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781* L_0 = ___0_snapshot;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		V_0 = L_1;
		bool L_2 = V_0;
		if (!L_2)
		{
			goto IL_0027;
		}
	}
	{
		String_t* L_3;
		L_3 = Object_get_name_mAC2F6B897CF1303BA4249B4CB55271AFACBB6392(__this, NULL);
		String_t* L_4;
		L_4 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral9EAFE4F84FECC3407E28F6C22DC663D4A31C78D0)), L_3, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_5 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_5, L_4, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_5, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AudioMixer_TransitionToSnapshot_m308E8D50A4573EFDD381ED7DEC23162F8CD0EB5D_RuntimeMethod_var)));
	}

IL_0027:
	{
		AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781* L_6 = ___0_snapshot;
		AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* L_7;
		L_7 = AudioMixerSnapshot_get_audioMixer_mCC13199F7F9EBB2C5510DFA750A501747229CBF5(L_6, NULL);
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_8;
		L_8 = Object_op_Inequality_mD0BE578448EAA61948F25C32F8DD55AB1F778602(L_7, __this, NULL);
		V_1 = L_8;
		bool L_9 = V_1;
		if (!L_9)
		{
			goto IL_0072;
		}
	}
	{
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_10 = (StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248*)(StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248*)SZArrayNew(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var)), (uint32_t)5);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_11 = L_10;
		(L_11)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(0), (String_t*)((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralC01DE08865A1C5D9A0B7F1023D8CD55C8685904B)));
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_12 = L_11;
		AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781* L_13 = ___0_snapshot;
		String_t* L_14;
		L_14 = Object_get_name_mAC2F6B897CF1303BA4249B4CB55271AFACBB6392(L_13, NULL);
		(L_12)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(1), (String_t*)L_14);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_15 = L_12;
		(L_15)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(2), (String_t*)((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralB1FAE4F904170535BC9DC23FF347B1D0603313EA)));
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_16 = L_15;
		String_t* L_17;
		L_17 = Object_get_name_mAC2F6B897CF1303BA4249B4CB55271AFACBB6392(__this, NULL);
		(L_16)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(3), (String_t*)L_17);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_18 = L_16;
		(L_18)->SetAtUnchecked(static_cast<il2cpp_array_size_t>(4), (String_t*)((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)));
		String_t* L_19;
		L_19 = String_Concat_m647EBF831F54B6DF7D5AFA5FD012CF4EE7571B6A(L_18, NULL);
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_20 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_20, L_19, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_20, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AudioMixer_TransitionToSnapshot_m308E8D50A4573EFDD381ED7DEC23162F8CD0EB5D_RuntimeMethod_var)));
	}

IL_0072:
	{
		AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781* L_21 = ___0_snapshot;
		float L_22 = ___1_timeToReach;
		AudioMixer_TransitionToSnapshotInternal_m9D07330A3EF997CC4BB165829D8D2AD6187A8DD0(__this, L_21, L_22, NULL);
		return;
	}
}
// Method Definition Index: 60225
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioMixer_TransitionToSnapshotInternal_m9D07330A3EF997CC4BB165829D8D2AD6187A8DD0 (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* __this, AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781* ___0_snapshot, float ___1_timeToReach, const RuntimeMethod* method) 
{
	typedef void (*AudioMixer_TransitionToSnapshotInternal_m9D07330A3EF997CC4BB165829D8D2AD6187A8DD0_ftn) (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04*, AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781*, float);
	static AudioMixer_TransitionToSnapshotInternal_m9D07330A3EF997CC4BB165829D8D2AD6187A8DD0_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioMixer_TransitionToSnapshotInternal_m9D07330A3EF997CC4BB165829D8D2AD6187A8DD0_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioMixer::TransitionToSnapshotInternal(UnityEngine.Audio.AudioMixerSnapshot,System.Single)");
	_il2cpp_icall_func(__this, ___0_snapshot, ___1_timeToReach);
}
// Method Definition Index: 60226
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioMixer_TransitionToSnapshots_m87D4E352A2696F6BF945EB1A519888A9B38BF7C6 (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* __this, AudioMixerSnapshotU5BU5D_tD41A94680B8A241063054F1931BDDC8B997D8382* ___0_snapshots, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___1_weights, float ___2_timeToReach, const RuntimeMethod* method) 
{
	typedef void (*AudioMixer_TransitionToSnapshots_m87D4E352A2696F6BF945EB1A519888A9B38BF7C6_ftn) (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04*, AudioMixerSnapshotU5BU5D_tD41A94680B8A241063054F1931BDDC8B997D8382*, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*, float);
	static AudioMixer_TransitionToSnapshots_m87D4E352A2696F6BF945EB1A519888A9B38BF7C6_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioMixer_TransitionToSnapshots_m87D4E352A2696F6BF945EB1A519888A9B38BF7C6_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioMixer::TransitionToSnapshots(UnityEngine.Audio.AudioMixerSnapshot[],System.Single[],System.Single)");
	_il2cpp_icall_func(__this, ___0_snapshots, ___1_weights, ___2_timeToReach);
}
// Method Definition Index: 60227
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AudioMixer_get_updateMode_mBA8246F84EE1B3F135D4E76B91128DB295CFE5F9 (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* __this, const RuntimeMethod* method) 
{
	typedef int32_t (*AudioMixer_get_updateMode_mBA8246F84EE1B3F135D4E76B91128DB295CFE5F9_ftn) (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04*);
	static AudioMixer_get_updateMode_mBA8246F84EE1B3F135D4E76B91128DB295CFE5F9_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioMixer_get_updateMode_mBA8246F84EE1B3F135D4E76B91128DB295CFE5F9_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioMixer::get_updateMode()");
	int32_t icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
// Method Definition Index: 60228
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioMixer_set_updateMode_mA823193DD10F7737E036013BB63CEC2ED65E184F (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	typedef void (*AudioMixer_set_updateMode_mA823193DD10F7737E036013BB63CEC2ED65E184F_ftn) (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04*, int32_t);
	static AudioMixer_set_updateMode_mA823193DD10F7737E036013BB63CEC2ED65E184F_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioMixer_set_updateMode_mA823193DD10F7737E036013BB63CEC2ED65E184F_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioMixer::set_updateMode(UnityEngine.Audio.AudioMixerUpdateMode)");
	_il2cpp_icall_func(__this, ___0_value);
}
// Method Definition Index: 60229
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioMixer_SetFloat_m4789959013BE79E4F84F446405914908ADC3F335 (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* __this, String_t* ___0_name, float ___1_value, const RuntimeMethod* method) 
{
	typedef bool (*AudioMixer_SetFloat_m4789959013BE79E4F84F446405914908ADC3F335_ftn) (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04*, String_t*, float);
	static AudioMixer_SetFloat_m4789959013BE79E4F84F446405914908ADC3F335_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioMixer_SetFloat_m4789959013BE79E4F84F446405914908ADC3F335_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioMixer::SetFloat(System.String,System.Single)");
	bool icallRetVal = _il2cpp_icall_func(__this, ___0_name, ___1_value);
	return icallRetVal;
}
// Method Definition Index: 60230
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioMixer_ClearFloat_mD6FD7AE99760D83DA6ECBCCF9A0F07F10C12E665 (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* __this, String_t* ___0_name, const RuntimeMethod* method) 
{
	typedef bool (*AudioMixer_ClearFloat_mD6FD7AE99760D83DA6ECBCCF9A0F07F10C12E665_ftn) (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04*, String_t*);
	static AudioMixer_ClearFloat_mD6FD7AE99760D83DA6ECBCCF9A0F07F10C12E665_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioMixer_ClearFloat_mD6FD7AE99760D83DA6ECBCCF9A0F07F10C12E665_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioMixer::ClearFloat(System.String)");
	bool icallRetVal = _il2cpp_icall_func(__this, ___0_name);
	return icallRetVal;
}
// Method Definition Index: 60231
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioMixer_GetFloat_mAED8D277AD30D0346292555CBF81D8961117AEC9 (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* __this, String_t* ___0_name, float* ___1_value, const RuntimeMethod* method) 
{
	typedef bool (*AudioMixer_GetFloat_mAED8D277AD30D0346292555CBF81D8961117AEC9_ftn) (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04*, String_t*, float*);
	static AudioMixer_GetFloat_mAED8D277AD30D0346292555CBF81D8961117AEC9_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioMixer_GetFloat_mAED8D277AD30D0346292555CBF81D8961117AEC9_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioMixer::GetFloat(System.String,System.Single&)");
	bool icallRetVal = _il2cpp_icall_func(__this, ___0_name, ___1_value);
	return icallRetVal;
}
// Method Definition Index: 60232
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AudioMixer_GetAbsoluteAudibilityFromGroup_m109E7C4D1ECABCB80EB5F7C1952ACDD77986772D (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* __this, AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311* ___0_group, const RuntimeMethod* method) 
{
	typedef float (*AudioMixer_GetAbsoluteAudibilityFromGroup_m109E7C4D1ECABCB80EB5F7C1952ACDD77986772D_ftn) (AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04*, AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311*);
	static AudioMixer_GetAbsoluteAudibilityFromGroup_m109E7C4D1ECABCB80EB5F7C1952ACDD77986772D_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioMixer_GetAbsoluteAudibilityFromGroup_m109E7C4D1ECABCB80EB5F7C1952ACDD77986772D_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioMixer::GetAbsoluteAudibilityFromGroup(UnityEngine.Audio.AudioMixerGroup)");
	float icallRetVal = _il2cpp_icall_func(__this, ___0_group);
	return icallRetVal;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 60233
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioMixerGroup__ctor_m0D3A84EDAC9B01AEC0B07AFB1F5B1807F74B9CB8 (AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		Object__ctor_m2149FA40CEC8D82AC20D3508AB40C0D8EFEF68E6(__this, NULL);
		return;
	}
}
// Method Definition Index: 60234
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* AudioMixerGroup_get_audioMixer_mFDEDBF17C3B84C6B777D2BF75CF40EECF4C889E4 (AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311* __this, const RuntimeMethod* method) 
{
	typedef AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* (*AudioMixerGroup_get_audioMixer_mFDEDBF17C3B84C6B777D2BF75CF40EECF4C889E4_ftn) (AudioMixerGroup_tD29AC8336F7425DF007944F8195CEABF34FC3311*);
	static AudioMixerGroup_get_audioMixer_mFDEDBF17C3B84C6B777D2BF75CF40EECF4C889E4_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioMixerGroup_get_audioMixer_mFDEDBF17C3B84C6B777D2BF75CF40EECF4C889E4_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioMixerGroup::get_audioMixer()");
	AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 60235
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 AudioMixerPlayable_GetHandle_m6C182D9794E901D123223BB57738A302BEAB41FD (AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C* __this, const RuntimeMethod* method) 
{
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_0 = __this->___m_Handle;
		V_0 = L_0;
		goto IL_000a;
	}

IL_000a:
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_1 = V_0;
		return L_1;
	}
}
IL2CPP_EXTERN_C  PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 AudioMixerPlayable_GetHandle_m6C182D9794E901D123223BB57738A302BEAB41FD_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C*>(__this + _offset);
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 _returnValue;
	_returnValue = AudioMixerPlayable_GetHandle_m6C182D9794E901D123223BB57738A302BEAB41FD(_thisAdjusted, method);
	return _returnValue;
}
// Method Definition Index: 60236
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioMixerPlayable_Equals_mDFB945EB48199A338BAD00D40FB8EEC34CF64D57 (AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C* __this, AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C ___0_other, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_0;
		L_0 = AudioMixerPlayable_GetHandle_m6C182D9794E901D123223BB57738A302BEAB41FD(__this, NULL);
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_1;
		L_1 = AudioMixerPlayable_GetHandle_m6C182D9794E901D123223BB57738A302BEAB41FD((&___0_other), NULL);
		il2cpp_codegen_runtime_class_init_inline(PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_il2cpp_TypeInfo_var);
		bool L_2;
		L_2 = PlayableHandle_op_Equality_m0E6C48A28F75A870AC22ADE3BD42F7F70A43C99C(L_0, L_1, NULL);
		V_0 = L_2;
		goto IL_0016;
	}

IL_0016:
	{
		bool L_3 = V_0;
		return L_3;
	}
}
IL2CPP_EXTERN_C  bool AudioMixerPlayable_Equals_mDFB945EB48199A338BAD00D40FB8EEC34CF64D57_AdjustorThunk (RuntimeObject* __this, AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C ___0_other, const RuntimeMethod* method)
{
	AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<AudioMixerPlayable_t6AADDF0C53DF1B4C17969EC24B3B4E4975F3A56C*>(__this + _offset);
	bool _returnValue;
	_returnValue = AudioMixerPlayable_Equals_mDFB945EB48199A338BAD00D40FB8EEC34CF64D57(_thisAdjusted, ___0_other, method);
	return _returnValue;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 60237
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioMixerSnapshot__ctor_m68E824AB2B201928EABE2FFCDDC401EE905D2D06 (AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		Object__ctor_m2149FA40CEC8D82AC20D3508AB40C0D8EFEF68E6(__this, NULL);
		return;
	}
}
// Method Definition Index: 60238
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* AudioMixerSnapshot_get_audioMixer_mCC13199F7F9EBB2C5510DFA750A501747229CBF5 (AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781* __this, const RuntimeMethod* method) 
{
	typedef AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* (*AudioMixerSnapshot_get_audioMixer_mCC13199F7F9EBB2C5510DFA750A501747229CBF5_ftn) (AudioMixerSnapshot_tB9A62E6CFA52643B938E4FBFFAE1A5ED30907781*);
	static AudioMixerSnapshot_get_audioMixer_mCC13199F7F9EBB2C5510DFA750A501747229CBF5_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AudioMixerSnapshot_get_audioMixer_mCC13199F7F9EBB2C5510DFA750A501747229CBF5_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.Audio.AudioMixerSnapshot::get_audioMixer()");
	AudioMixer_tE2E8D79241711CDF9AB428C7FB96A35D80E40B04* icallRetVal = _il2cpp_icall_func(__this);
	return icallRetVal;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
// Method Definition Index: 60158
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioConfigurationChangeHandler_Invoke_m4DC27DD11512481B60071B20284E6886DAE54DE2_inline (AudioConfigurationChangeHandler_tE071B0CBA3B3A77D3E41F5FCB65B4017885B3177* __this, bool ___0_deviceWasChanged, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, bool, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_deviceWasChanged, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 861
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* __this, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 60192
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void PCMReaderCallback_Invoke_m76784C690C36B513E2AA5B0E4FD9831B2C7E5152_inline (PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* __this, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___0_data, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_data, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 60194
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void PCMSetPositionCallback_Invoke_m434D4F02FA25F91DF6199EC5A799C551C7F93702_inline (PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* __this, int32_t ___0_position, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, int32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_position, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 60216
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void SampleFramesHandler_Invoke_m478D5645634B8C734E58B59CF7750797FC54F1BC_inline (SampleFramesHandler_tFE84FF9BBCEFB880D46227188F375BEF680AAA30* __this, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2* ___0_provider, uint32_t ___1_sampleFrameCount, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, AudioSampleProvider_t602353124A2F6F2AEC38E56C3C21932344F712E2*, uint32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_provider, ___1_sampleFrameCount, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
