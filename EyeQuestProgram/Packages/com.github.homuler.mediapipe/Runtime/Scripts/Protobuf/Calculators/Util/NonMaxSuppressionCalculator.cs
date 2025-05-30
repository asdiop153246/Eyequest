// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: mediapipe/calculators/util/non_max_suppression_calculator.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Mediapipe {

  /// <summary>Holder for reflection information generated from mediapipe/calculators/util/non_max_suppression_calculator.proto</summary>
  public static partial class NonMaxSuppressionCalculatorReflection {

    #region Descriptor
    /// <summary>File descriptor for mediapipe/calculators/util/non_max_suppression_calculator.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static NonMaxSuppressionCalculatorReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cj9tZWRpYXBpcGUvY2FsY3VsYXRvcnMvdXRpbC9ub25fbWF4X3N1cHByZXNz",
            "aW9uX2NhbGN1bGF0b3IucHJvdG8SCW1lZGlhcGlwZRokbWVkaWFwaXBlL2Zy",
            "YW1ld29yay9jYWxjdWxhdG9yLnByb3RvIpQFCiJOb25NYXhTdXBwcmVzc2lv",
            "bkNhbGN1bGF0b3JPcHRpb25zEiAKFW51bV9kZXRlY3Rpb25fc3RyZWFtcxgB",
            "IAEoBToBMRIeChJtYXhfbnVtX2RldGVjdGlvbnMYAiABKAU6Ai0xEh8KE21p",
            "bl9zY29yZV90aHJlc2hvbGQYBiABKAI6Ai0xEiQKGW1pbl9zdXBwcmVzc2lv",
            "bl90aHJlc2hvbGQYAyABKAI6ATESWAoMb3ZlcmxhcF90eXBlGAQgASgOMjku",
            "bWVkaWFwaXBlLk5vbk1heFN1cHByZXNzaW9uQ2FsY3VsYXRvck9wdGlvbnMu",
            "T3ZlcmxhcFR5cGU6B0pBQ0NBUkQSHwoXcmV0dXJuX2VtcHR5X2RldGVjdGlv",
            "bnMYBSABKAgSVgoJYWxnb3JpdGhtGAcgASgOMjoubWVkaWFwaXBlLk5vbk1h",
            "eFN1cHByZXNzaW9uQ2FsY3VsYXRvck9wdGlvbnMuTm1zQWxnb3JpdGhtOgdE",
            "RUZBVUxUEh0KDm11bHRpY2xhc3Nfbm1zGAggASgIOgVmYWxzZSJrCgtPdmVy",
            "bGFwVHlwZRIcChhVTlNQRUNJRklFRF9PVkVSTEFQX1RZUEUQABILCgdKQUND",
            "QVJEEAESFAoQTU9ESUZJRURfSkFDQ0FSRBACEhsKF0lOVEVSU0VDVElPTl9P",
            "VkVSX1VOSU9OEAMiKQoMTm1zQWxnb3JpdGhtEgsKB0RFRkFVTFQQABIMCghX",
            "RUlHSFRFRBABMlsKA2V4dBIcLm1lZGlhcGlwZS5DYWxjdWxhdG9yT3B0aW9u",
            "cxi8qLQaIAEoCzItLm1lZGlhcGlwZS5Ob25NYXhTdXBwcmVzc2lvbkNhbGN1",
            "bGF0b3JPcHRpb25z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Mediapipe.CalculatorReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Mediapipe.NonMaxSuppressionCalculatorOptions), global::Mediapipe.NonMaxSuppressionCalculatorOptions.Parser, new[]{ "NumDetectionStreams", "MaxNumDetections", "MinScoreThreshold", "MinSuppressionThreshold", "OverlapType", "ReturnEmptyDetections", "Algorithm", "MulticlassNms" }, null, new[]{ typeof(global::Mediapipe.NonMaxSuppressionCalculatorOptions.Types.OverlapType), typeof(global::Mediapipe.NonMaxSuppressionCalculatorOptions.Types.NmsAlgorithm) }, new pb::Extension[] { global::Mediapipe.NonMaxSuppressionCalculatorOptions.Extensions.Ext }, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  /// Options to NonMaxSuppression calculator, which performs non-maximum
  /// suppression on a set of detections.
  /// </summary>
  public sealed partial class NonMaxSuppressionCalculatorOptions : pb::IMessage<NonMaxSuppressionCalculatorOptions>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<NonMaxSuppressionCalculatorOptions> _parser = new pb::MessageParser<NonMaxSuppressionCalculatorOptions>(() => new NonMaxSuppressionCalculatorOptions());
    private pb::UnknownFieldSet _unknownFields;
    private int _hasBits0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<NonMaxSuppressionCalculatorOptions> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Mediapipe.NonMaxSuppressionCalculatorReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public NonMaxSuppressionCalculatorOptions() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public NonMaxSuppressionCalculatorOptions(NonMaxSuppressionCalculatorOptions other) : this() {
      _hasBits0 = other._hasBits0;
      numDetectionStreams_ = other.numDetectionStreams_;
      maxNumDetections_ = other.maxNumDetections_;
      minScoreThreshold_ = other.minScoreThreshold_;
      minSuppressionThreshold_ = other.minSuppressionThreshold_;
      overlapType_ = other.overlapType_;
      returnEmptyDetections_ = other.returnEmptyDetections_;
      algorithm_ = other.algorithm_;
      multiclassNms_ = other.multiclassNms_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public NonMaxSuppressionCalculatorOptions Clone() {
      return new NonMaxSuppressionCalculatorOptions(this);
    }

    /// <summary>Field number for the "num_detection_streams" field.</summary>
    public const int NumDetectionStreamsFieldNumber = 1;
    private readonly static int NumDetectionStreamsDefaultValue = 1;

    private int numDetectionStreams_;
    /// <summary>
    /// Number of input streams. Each input stream should contain a vector of
    /// detections.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int NumDetectionStreams {
      get { if ((_hasBits0 & 1) != 0) { return numDetectionStreams_; } else { return NumDetectionStreamsDefaultValue; } }
      set {
        _hasBits0 |= 1;
        numDetectionStreams_ = value;
      }
    }
    /// <summary>Gets whether the "num_detection_streams" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasNumDetectionStreams {
      get { return (_hasBits0 & 1) != 0; }
    }
    /// <summary>Clears the value of the "num_detection_streams" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearNumDetectionStreams() {
      _hasBits0 &= ~1;
    }

    /// <summary>Field number for the "max_num_detections" field.</summary>
    public const int MaxNumDetectionsFieldNumber = 2;
    private readonly static int MaxNumDetectionsDefaultValue = -1;

    private int maxNumDetections_;
    /// <summary>
    /// Maximum number of detections to be returned. If -1, then all detections are
    /// returned.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int MaxNumDetections {
      get { if ((_hasBits0 & 2) != 0) { return maxNumDetections_; } else { return MaxNumDetectionsDefaultValue; } }
      set {
        _hasBits0 |= 2;
        maxNumDetections_ = value;
      }
    }
    /// <summary>Gets whether the "max_num_detections" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasMaxNumDetections {
      get { return (_hasBits0 & 2) != 0; }
    }
    /// <summary>Clears the value of the "max_num_detections" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearMaxNumDetections() {
      _hasBits0 &= ~2;
    }

    /// <summary>Field number for the "min_score_threshold" field.</summary>
    public const int MinScoreThresholdFieldNumber = 6;
    private readonly static float MinScoreThresholdDefaultValue = -1F;

    private float minScoreThreshold_;
    /// <summary>
    /// Minimum score of detections to be returned.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public float MinScoreThreshold {
      get { if ((_hasBits0 & 32) != 0) { return minScoreThreshold_; } else { return MinScoreThresholdDefaultValue; } }
      set {
        _hasBits0 |= 32;
        minScoreThreshold_ = value;
      }
    }
    /// <summary>Gets whether the "min_score_threshold" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasMinScoreThreshold {
      get { return (_hasBits0 & 32) != 0; }
    }
    /// <summary>Clears the value of the "min_score_threshold" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearMinScoreThreshold() {
      _hasBits0 &= ~32;
    }

    /// <summary>Field number for the "min_suppression_threshold" field.</summary>
    public const int MinSuppressionThresholdFieldNumber = 3;
    private readonly static float MinSuppressionThresholdDefaultValue = 1F;

    private float minSuppressionThreshold_;
    /// <summary>
    /// Jaccard similarity threshold for suppression -- a detection would suppress
    /// all other detections whose scores are lower and overlap by at least the
    /// specified threshold.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public float MinSuppressionThreshold {
      get { if ((_hasBits0 & 4) != 0) { return minSuppressionThreshold_; } else { return MinSuppressionThresholdDefaultValue; } }
      set {
        _hasBits0 |= 4;
        minSuppressionThreshold_ = value;
      }
    }
    /// <summary>Gets whether the "min_suppression_threshold" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasMinSuppressionThreshold {
      get { return (_hasBits0 & 4) != 0; }
    }
    /// <summary>Clears the value of the "min_suppression_threshold" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearMinSuppressionThreshold() {
      _hasBits0 &= ~4;
    }

    /// <summary>Field number for the "overlap_type" field.</summary>
    public const int OverlapTypeFieldNumber = 4;
    private readonly static global::Mediapipe.NonMaxSuppressionCalculatorOptions.Types.OverlapType OverlapTypeDefaultValue = global::Mediapipe.NonMaxSuppressionCalculatorOptions.Types.OverlapType.Jaccard;

    private global::Mediapipe.NonMaxSuppressionCalculatorOptions.Types.OverlapType overlapType_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Mediapipe.NonMaxSuppressionCalculatorOptions.Types.OverlapType OverlapType {
      get { if ((_hasBits0 & 8) != 0) { return overlapType_; } else { return OverlapTypeDefaultValue; } }
      set {
        _hasBits0 |= 8;
        overlapType_ = value;
      }
    }
    /// <summary>Gets whether the "overlap_type" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasOverlapType {
      get { return (_hasBits0 & 8) != 0; }
    }
    /// <summary>Clears the value of the "overlap_type" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearOverlapType() {
      _hasBits0 &= ~8;
    }

    /// <summary>Field number for the "return_empty_detections" field.</summary>
    public const int ReturnEmptyDetectionsFieldNumber = 5;
    private readonly static bool ReturnEmptyDetectionsDefaultValue = false;

    private bool returnEmptyDetections_;
    /// <summary>
    /// Whether to put empty detection vector in output stream.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool ReturnEmptyDetections {
      get { if ((_hasBits0 & 16) != 0) { return returnEmptyDetections_; } else { return ReturnEmptyDetectionsDefaultValue; } }
      set {
        _hasBits0 |= 16;
        returnEmptyDetections_ = value;
      }
    }
    /// <summary>Gets whether the "return_empty_detections" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasReturnEmptyDetections {
      get { return (_hasBits0 & 16) != 0; }
    }
    /// <summary>Clears the value of the "return_empty_detections" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearReturnEmptyDetections() {
      _hasBits0 &= ~16;
    }

    /// <summary>Field number for the "algorithm" field.</summary>
    public const int AlgorithmFieldNumber = 7;
    private readonly static global::Mediapipe.NonMaxSuppressionCalculatorOptions.Types.NmsAlgorithm AlgorithmDefaultValue = global::Mediapipe.NonMaxSuppressionCalculatorOptions.Types.NmsAlgorithm.Default;

    private global::Mediapipe.NonMaxSuppressionCalculatorOptions.Types.NmsAlgorithm algorithm_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Mediapipe.NonMaxSuppressionCalculatorOptions.Types.NmsAlgorithm Algorithm {
      get { if ((_hasBits0 & 64) != 0) { return algorithm_; } else { return AlgorithmDefaultValue; } }
      set {
        _hasBits0 |= 64;
        algorithm_ = value;
      }
    }
    /// <summary>Gets whether the "algorithm" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasAlgorithm {
      get { return (_hasBits0 & 64) != 0; }
    }
    /// <summary>Clears the value of the "algorithm" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearAlgorithm() {
      _hasBits0 &= ~64;
    }

    /// <summary>Field number for the "multiclass_nms" field.</summary>
    public const int MulticlassNmsFieldNumber = 8;
    private readonly static bool MulticlassNmsDefaultValue = false;

    private bool multiclassNms_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool MulticlassNms {
      get { if ((_hasBits0 & 128) != 0) { return multiclassNms_; } else { return MulticlassNmsDefaultValue; } }
      set {
        _hasBits0 |= 128;
        multiclassNms_ = value;
      }
    }
    /// <summary>Gets whether the "multiclass_nms" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasMulticlassNms {
      get { return (_hasBits0 & 128) != 0; }
    }
    /// <summary>Clears the value of the "multiclass_nms" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearMulticlassNms() {
      _hasBits0 &= ~128;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as NonMaxSuppressionCalculatorOptions);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(NonMaxSuppressionCalculatorOptions other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (NumDetectionStreams != other.NumDetectionStreams) return false;
      if (MaxNumDetections != other.MaxNumDetections) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(MinScoreThreshold, other.MinScoreThreshold)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(MinSuppressionThreshold, other.MinSuppressionThreshold)) return false;
      if (OverlapType != other.OverlapType) return false;
      if (ReturnEmptyDetections != other.ReturnEmptyDetections) return false;
      if (Algorithm != other.Algorithm) return false;
      if (MulticlassNms != other.MulticlassNms) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (HasNumDetectionStreams) hash ^= NumDetectionStreams.GetHashCode();
      if (HasMaxNumDetections) hash ^= MaxNumDetections.GetHashCode();
      if (HasMinScoreThreshold) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(MinScoreThreshold);
      if (HasMinSuppressionThreshold) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(MinSuppressionThreshold);
      if (HasOverlapType) hash ^= OverlapType.GetHashCode();
      if (HasReturnEmptyDetections) hash ^= ReturnEmptyDetections.GetHashCode();
      if (HasAlgorithm) hash ^= Algorithm.GetHashCode();
      if (HasMulticlassNms) hash ^= MulticlassNms.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (HasNumDetectionStreams) {
        output.WriteRawTag(8);
        output.WriteInt32(NumDetectionStreams);
      }
      if (HasMaxNumDetections) {
        output.WriteRawTag(16);
        output.WriteInt32(MaxNumDetections);
      }
      if (HasMinSuppressionThreshold) {
        output.WriteRawTag(29);
        output.WriteFloat(MinSuppressionThreshold);
      }
      if (HasOverlapType) {
        output.WriteRawTag(32);
        output.WriteEnum((int) OverlapType);
      }
      if (HasReturnEmptyDetections) {
        output.WriteRawTag(40);
        output.WriteBool(ReturnEmptyDetections);
      }
      if (HasMinScoreThreshold) {
        output.WriteRawTag(53);
        output.WriteFloat(MinScoreThreshold);
      }
      if (HasAlgorithm) {
        output.WriteRawTag(56);
        output.WriteEnum((int) Algorithm);
      }
      if (HasMulticlassNms) {
        output.WriteRawTag(64);
        output.WriteBool(MulticlassNms);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (HasNumDetectionStreams) {
        output.WriteRawTag(8);
        output.WriteInt32(NumDetectionStreams);
      }
      if (HasMaxNumDetections) {
        output.WriteRawTag(16);
        output.WriteInt32(MaxNumDetections);
      }
      if (HasMinSuppressionThreshold) {
        output.WriteRawTag(29);
        output.WriteFloat(MinSuppressionThreshold);
      }
      if (HasOverlapType) {
        output.WriteRawTag(32);
        output.WriteEnum((int) OverlapType);
      }
      if (HasReturnEmptyDetections) {
        output.WriteRawTag(40);
        output.WriteBool(ReturnEmptyDetections);
      }
      if (HasMinScoreThreshold) {
        output.WriteRawTag(53);
        output.WriteFloat(MinScoreThreshold);
      }
      if (HasAlgorithm) {
        output.WriteRawTag(56);
        output.WriteEnum((int) Algorithm);
      }
      if (HasMulticlassNms) {
        output.WriteRawTag(64);
        output.WriteBool(MulticlassNms);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (HasNumDetectionStreams) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(NumDetectionStreams);
      }
      if (HasMaxNumDetections) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(MaxNumDetections);
      }
      if (HasMinScoreThreshold) {
        size += 1 + 4;
      }
      if (HasMinSuppressionThreshold) {
        size += 1 + 4;
      }
      if (HasOverlapType) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) OverlapType);
      }
      if (HasReturnEmptyDetections) {
        size += 1 + 1;
      }
      if (HasAlgorithm) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Algorithm);
      }
      if (HasMulticlassNms) {
        size += 1 + 1;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(NonMaxSuppressionCalculatorOptions other) {
      if (other == null) {
        return;
      }
      if (other.HasNumDetectionStreams) {
        NumDetectionStreams = other.NumDetectionStreams;
      }
      if (other.HasMaxNumDetections) {
        MaxNumDetections = other.MaxNumDetections;
      }
      if (other.HasMinScoreThreshold) {
        MinScoreThreshold = other.MinScoreThreshold;
      }
      if (other.HasMinSuppressionThreshold) {
        MinSuppressionThreshold = other.MinSuppressionThreshold;
      }
      if (other.HasOverlapType) {
        OverlapType = other.OverlapType;
      }
      if (other.HasReturnEmptyDetections) {
        ReturnEmptyDetections = other.ReturnEmptyDetections;
      }
      if (other.HasAlgorithm) {
        Algorithm = other.Algorithm;
      }
      if (other.HasMulticlassNms) {
        MulticlassNms = other.MulticlassNms;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            NumDetectionStreams = input.ReadInt32();
            break;
          }
          case 16: {
            MaxNumDetections = input.ReadInt32();
            break;
          }
          case 29: {
            MinSuppressionThreshold = input.ReadFloat();
            break;
          }
          case 32: {
            OverlapType = (global::Mediapipe.NonMaxSuppressionCalculatorOptions.Types.OverlapType) input.ReadEnum();
            break;
          }
          case 40: {
            ReturnEmptyDetections = input.ReadBool();
            break;
          }
          case 53: {
            MinScoreThreshold = input.ReadFloat();
            break;
          }
          case 56: {
            Algorithm = (global::Mediapipe.NonMaxSuppressionCalculatorOptions.Types.NmsAlgorithm) input.ReadEnum();
            break;
          }
          case 64: {
            MulticlassNms = input.ReadBool();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            NumDetectionStreams = input.ReadInt32();
            break;
          }
          case 16: {
            MaxNumDetections = input.ReadInt32();
            break;
          }
          case 29: {
            MinSuppressionThreshold = input.ReadFloat();
            break;
          }
          case 32: {
            OverlapType = (global::Mediapipe.NonMaxSuppressionCalculatorOptions.Types.OverlapType) input.ReadEnum();
            break;
          }
          case 40: {
            ReturnEmptyDetections = input.ReadBool();
            break;
          }
          case 53: {
            MinScoreThreshold = input.ReadFloat();
            break;
          }
          case 56: {
            Algorithm = (global::Mediapipe.NonMaxSuppressionCalculatorOptions.Types.NmsAlgorithm) input.ReadEnum();
            break;
          }
          case 64: {
            MulticlassNms = input.ReadBool();
            break;
          }
        }
      }
    }
    #endif

    #region Nested types
    /// <summary>Container for nested types declared in the NonMaxSuppressionCalculatorOptions message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static partial class Types {
      /// <summary>
      /// During the overlap computation, which is used to determine whether a
      /// rectangle suppresses another rectangle, one can use the Jaccard similarity,
      /// defined as the ration of the intersection over union of the two rectangles.
      /// Alternatively a modified version of Jaccard can be used, where the
      /// normalization is done by the area of the rectangle being checked for
      /// suppression.
      /// </summary>
      public enum OverlapType {
        [pbr::OriginalName("UNSPECIFIED_OVERLAP_TYPE")] UnspecifiedOverlapType = 0,
        [pbr::OriginalName("JACCARD")] Jaccard = 1,
        [pbr::OriginalName("MODIFIED_JACCARD")] ModifiedJaccard = 2,
        [pbr::OriginalName("INTERSECTION_OVER_UNION")] IntersectionOverUnion = 3,
      }

      /// <summary>
      /// Algorithms that can be used to apply non-maximum suppression.
      /// </summary>
      public enum NmsAlgorithm {
        [pbr::OriginalName("DEFAULT")] Default = 0,
        /// <summary>
        /// Only supports relative bounding box for weighted NMS.
        /// </summary>
        [pbr::OriginalName("WEIGHTED")] Weighted = 1,
      }

    }
    #endregion

    #region Extensions
    /// <summary>Container for extensions for other messages declared in the NonMaxSuppressionCalculatorOptions message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static partial class Extensions {
      public static readonly pb::Extension<global::Mediapipe.CalculatorOptions, global::Mediapipe.NonMaxSuppressionCalculatorOptions> Ext =
        new pb::Extension<global::Mediapipe.CalculatorOptions, global::Mediapipe.NonMaxSuppressionCalculatorOptions>(55383100, pb::FieldCodec.ForMessage(443064802, global::Mediapipe.NonMaxSuppressionCalculatorOptions.Parser));
    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code
