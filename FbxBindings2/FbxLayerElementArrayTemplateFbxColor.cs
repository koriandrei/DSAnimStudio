//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace Unity.FbxSdk {

public class FbxLayerElementArrayTemplateFbxColor : FbxLayerElementArray {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal FbxLayerElementArrayTemplateFbxColor(global::System.IntPtr cPtr, bool cMemoryOwn) : base(GlobalsPINVOKE.FbxLayerElementArrayTemplateFbxColor_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(FbxLayerElementArrayTemplateFbxColor obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~FbxLayerElementArrayTemplateFbxColor() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          GlobalsPINVOKE.delete_FbxLayerElementArrayTemplateFbxColor(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  private FbxColor GetAtUnchecked(int pIndex) {
    var ret = GlobalsPINVOKE.FbxLayerElementArrayTemplateFbxColor_GetAtUnchecked(swigCPtr, pIndex);
    if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

   public FbxColor GetAt(int pIndex) { 
      if (pIndex < 0 || pIndex >= GetCount()) { 
        throw new System.IndexOutOfRangeException();
      }
      return GetAtUnchecked(pIndex);
    }

  public FbxLayerElementArrayTemplateFbxColor() : this(GlobalsPINVOKE.new_FbxLayerElementArrayTemplateFbxColor(), true) {
  }

}

}
