using System;


namespace Assets.Scripts.Interfaces
{
    public interface IGroundLandable<T>
    {
        void OnTouchGround();
        T GetController();
    }
}
