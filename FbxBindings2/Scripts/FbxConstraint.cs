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

public class FbxConstraint : FbxObject {
  internal FbxConstraint(global::System.IntPtr cPtr, bool ignored) : base(cPtr, ignored) { }

  // override void Dispose() {base.Dispose();}

  public new static FbxConstraint Create(FbxManager pManager, string pName) {
    global::System.IntPtr cPtr = GlobalsPINVOKE.FbxConstraint_Create__SWIG_0(FbxManager.getCPtr(pManager), pName);
    FbxConstraint ret = (cPtr == global::System.IntPtr.Zero) ? null : new FbxConstraint(cPtr, false);
    if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public new static FbxConstraint Create(FbxObject pContainer, string pName) {
    global::System.IntPtr cPtr = GlobalsPINVOKE.FbxConstraint_Create__SWIG_1(FbxObject.getCPtr(pContainer), pName);
    FbxConstraint ret = (cPtr == global::System.IntPtr.Zero) ? null : new FbxConstraint(cPtr, false);
    if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public FbxPropertyDouble Weight {
    get {
      FbxPropertyDouble ret = new FbxPropertyDouble(GlobalsPINVOKE.FbxConstraint_Weight_get(swigCPtr), false);
      if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public FbxPropertyBool Active {
    get {
      FbxPropertyBool ret = new FbxPropertyBool(GlobalsPINVOKE.FbxConstraint_Active_get(swigCPtr), false);
      if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public FbxPropertyBool Lock {
    get {
      FbxPropertyBool ret = new FbxPropertyBool(GlobalsPINVOKE.FbxConstraint_Lock_get(swigCPtr), false);
      if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public virtual FbxConstraint.EType GetConstraintType() {
    FbxConstraint.EType ret = (FbxConstraint.EType)GlobalsPINVOKE.FbxConstraint_GetConstraintType(swigCPtr);
    if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual FbxObject GetConstrainedObject() {
    global::System.IntPtr cPtr = GlobalsPINVOKE.FbxConstraint_GetConstrainedObject(swigCPtr);
    FbxObject ret = (cPtr == global::System.IntPtr.Zero) ? null : new FbxObject(cPtr, false);
    if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual int GetConstraintSourceCount() {
    int ret = GlobalsPINVOKE.FbxConstraint_GetConstraintSourceCount(swigCPtr);
    if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual FbxObject GetConstraintSource(int arg0) {
    global::System.IntPtr cPtr = GlobalsPINVOKE.FbxConstraint_GetConstraintSource(swigCPtr, arg0);
    FbxObject ret = (cPtr == global::System.IntPtr.Zero) ? null : new FbxObject(cPtr, false);
    if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetSourceWeight(FbxObject pObject) {
    double ret = GlobalsPINVOKE.FbxConstraint_GetSourceWeight(swigCPtr, FbxObject.getCPtr(pObject));
    if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override int GetHashCode(){
      return swigCPtr.Handle.GetHashCode();
  }

  public bool Equals(FbxConstraint other) {
    if (object.ReferenceEquals(other, null)) { return false; }
    return this.swigCPtr.Handle.Equals (other.swigCPtr.Handle);
  }

  public override bool Equals(object obj){
    if (object.ReferenceEquals(obj, null)) { return false; }
    /* is obj a subclass of this type; if so use our Equals */
    var typed = obj as FbxConstraint;
    if (!object.ReferenceEquals(typed, null)) {
      return this.Equals(typed);
    }
    /* are we a subclass of the other type; if so use their Equals */
    if (typeof(FbxConstraint).IsSubclassOf(obj.GetType())) {
      return obj.Equals(this);
    }
    /* types are unrelated; can't be a match */
    return false;
  }

  public static bool operator == (FbxConstraint a, FbxConstraint b) {
    if (object.ReferenceEquals(a, b)) { return true; }
    if (object.ReferenceEquals(a, null) || object.ReferenceEquals(b, null)) { return false; }
    return a.Equals(b);
  }

  public static bool operator != (FbxConstraint a, FbxConstraint b) {
    return !(a == b);
  }

  public enum EType {
    eUnknown,
    ePosition,
    eRotation,
    eScale,
    eParent,
    eSingleChainIK,
    eAim,
    eCharacter,
    eCustom
  }

}

}
