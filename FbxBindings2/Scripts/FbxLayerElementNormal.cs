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

public class FbxLayerElementNormal : FbxLayerElementTemplateFbxVector4 {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal FbxLayerElementNormal(global::System.IntPtr cPtr, bool cMemoryOwn) : base(GlobalsPINVOKE.FbxLayerElementNormal_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(FbxLayerElementNormal obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          throw new global::System.MethodAccessException("C++ destructor does not have public access");
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public static FbxLayerElementNormal Create(FbxLayerContainer pOwner, string pName) {
    global::System.IntPtr cPtr = GlobalsPINVOKE.FbxLayerElementNormal_Create(FbxLayerContainer.getCPtr(pOwner), pName);
    FbxLayerElementNormal ret = (cPtr == global::System.IntPtr.Zero) ? null : new FbxLayerElementNormal(cPtr, false);
    if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
