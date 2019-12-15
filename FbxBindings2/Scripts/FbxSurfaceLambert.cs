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

public class FbxSurfaceLambert : FbxSurfaceMaterial {
  internal FbxSurfaceLambert(global::System.IntPtr cPtr, bool ignored) : base(cPtr, ignored) { }

  // override void Dispose() {base.Dispose();}

  public new static FbxSurfaceLambert Create(FbxManager pManager, string pName) {
    global::System.IntPtr cPtr = GlobalsPINVOKE.FbxSurfaceLambert_Create__SWIG_0(FbxManager.getCPtr(pManager), pName);
    FbxSurfaceLambert ret = (cPtr == global::System.IntPtr.Zero) ? null : new FbxSurfaceLambert(cPtr, false);
    if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public new static FbxSurfaceLambert Create(FbxObject pContainer, string pName) {
    global::System.IntPtr cPtr = GlobalsPINVOKE.FbxSurfaceLambert_Create__SWIG_1(FbxObject.getCPtr(pContainer), pName);
    FbxSurfaceLambert ret = (cPtr == global::System.IntPtr.Zero) ? null : new FbxSurfaceLambert(cPtr, false);
    if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public FbxPropertyDouble3 Emissive {
    get {
      FbxPropertyDouble3 ret = new FbxPropertyDouble3(GlobalsPINVOKE.FbxSurfaceLambert_Emissive_get(swigCPtr), false);
      if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public FbxPropertyDouble EmissiveFactor {
    get {
      FbxPropertyDouble ret = new FbxPropertyDouble(GlobalsPINVOKE.FbxSurfaceLambert_EmissiveFactor_get(swigCPtr), false);
      if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public FbxPropertyDouble3 Ambient {
    get {
      FbxPropertyDouble3 ret = new FbxPropertyDouble3(GlobalsPINVOKE.FbxSurfaceLambert_Ambient_get(swigCPtr), false);
      if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public FbxPropertyDouble AmbientFactor {
    get {
      FbxPropertyDouble ret = new FbxPropertyDouble(GlobalsPINVOKE.FbxSurfaceLambert_AmbientFactor_get(swigCPtr), false);
      if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public FbxPropertyDouble3 Diffuse {
    get {
      FbxPropertyDouble3 ret = new FbxPropertyDouble3(GlobalsPINVOKE.FbxSurfaceLambert_Diffuse_get(swigCPtr), false);
      if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public FbxPropertyDouble DiffuseFactor {
    get {
      FbxPropertyDouble ret = new FbxPropertyDouble(GlobalsPINVOKE.FbxSurfaceLambert_DiffuseFactor_get(swigCPtr), false);
      if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public FbxPropertyDouble3 NormalMap {
    get {
      FbxPropertyDouble3 ret = new FbxPropertyDouble3(GlobalsPINVOKE.FbxSurfaceLambert_NormalMap_get(swigCPtr), false);
      if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public FbxPropertyDouble3 Bump {
    get {
      FbxPropertyDouble3 ret = new FbxPropertyDouble3(GlobalsPINVOKE.FbxSurfaceLambert_Bump_get(swigCPtr), false);
      if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public FbxPropertyDouble BumpFactor {
    get {
      FbxPropertyDouble ret = new FbxPropertyDouble(GlobalsPINVOKE.FbxSurfaceLambert_BumpFactor_get(swigCPtr), false);
      if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public FbxPropertyDouble3 TransparentColor {
    get {
      FbxPropertyDouble3 ret = new FbxPropertyDouble3(GlobalsPINVOKE.FbxSurfaceLambert_TransparentColor_get(swigCPtr), false);
      if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public FbxPropertyDouble TransparencyFactor {
    get {
      FbxPropertyDouble ret = new FbxPropertyDouble(GlobalsPINVOKE.FbxSurfaceLambert_TransparencyFactor_get(swigCPtr), false);
      if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public FbxPropertyDouble3 DisplacementColor {
    get {
      FbxPropertyDouble3 ret = new FbxPropertyDouble3(GlobalsPINVOKE.FbxSurfaceLambert_DisplacementColor_get(swigCPtr), false);
      if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public FbxPropertyDouble DisplacementFactor {
    get {
      FbxPropertyDouble ret = new FbxPropertyDouble(GlobalsPINVOKE.FbxSurfaceLambert_DisplacementFactor_get(swigCPtr), false);
      if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public FbxPropertyDouble3 VectorDisplacementColor {
    get {
      FbxPropertyDouble3 ret = new FbxPropertyDouble3(GlobalsPINVOKE.FbxSurfaceLambert_VectorDisplacementColor_get(swigCPtr), false);
      if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public FbxPropertyDouble VectorDisplacementFactor {
    get {
      FbxPropertyDouble ret = new FbxPropertyDouble(GlobalsPINVOKE.FbxSurfaceLambert_VectorDisplacementFactor_get(swigCPtr), false);
      if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public override int GetHashCode(){
      return swigCPtr.Handle.GetHashCode();
  }

  public bool Equals(FbxSurfaceLambert other) {
    if (object.ReferenceEquals(other, null)) { return false; }
    return this.swigCPtr.Handle.Equals (other.swigCPtr.Handle);
  }

  public override bool Equals(object obj){
    if (object.ReferenceEquals(obj, null)) { return false; }
    /* is obj a subclass of this type; if so use our Equals */
    var typed = obj as FbxSurfaceLambert;
    if (!object.ReferenceEquals(typed, null)) {
      return this.Equals(typed);
    }
    /* are we a subclass of the other type; if so use their Equals */
    if (typeof(FbxSurfaceLambert).IsSubclassOf(obj.GetType())) {
      return obj.Equals(this);
    }
    /* types are unrelated; can't be a match */
    return false;
  }

  public static bool operator == (FbxSurfaceLambert a, FbxSurfaceLambert b) {
    if (object.ReferenceEquals(a, b)) { return true; }
    if (object.ReferenceEquals(a, null) || object.ReferenceEquals(b, null)) { return false; }
    return a.Equals(b);
  }

  public static bool operator != (FbxSurfaceLambert a, FbxSurfaceLambert b) {
    return !(a == b);
  }

}

}
