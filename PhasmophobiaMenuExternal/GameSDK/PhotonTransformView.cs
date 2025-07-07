namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PhotonTransformView
    {
        public PhotonTransformView(IntPtr pointer)
        {
            PhotonTransformViewPointer = pointer;
        }

        public IntPtr PhotonTransformViewPointer;
    }
}