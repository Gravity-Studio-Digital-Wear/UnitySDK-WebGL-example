using System;
using System.ComponentModel;
using UnityEngine;

namespace GravityTS.Utils.RPM
{
    public enum OutfitGender
    {
        [Description("masculine")]
        Masculine,
        [Description("feminine")]
        Feminine
    }

    public static class RPMPostProcessing
    {

        // Animation avatars
        private const string MaleAnimationAvatarName = "AnimationAvatars/MaleAnimationAvatar";
        private const string FemaleAnimationAvatarName = "AnimationAvatars/FemaleAnimationAvatar";

        // Animation controller
        private const string AnimatorControllerName = "Avatar Animator";

        // Bone names
        private const string Hips = "Hips";
        private const string Armature = "Armature";
        private const string LeftUpLeg = "Hips/LeftUpLeg";
        private const string Spine = "Hips/Spine/Spine1/Spine2";

        private static OutfitGender OutfitGender;

        public static void PrepareAvatar(GameObject avatar)
        {
            OutfitGender = OutfitGender.Masculine;

            RestructureAndSetAnimator(avatar);
            //SetAvatarAssetNames(avatar);
        }

        public static void RestructureAndSetAnimator(GameObject avatar)
        {
            #region Restructure
            GameObject armature = new GameObject();
            armature.name = Armature;

            armature.transform.parent = avatar.transform;

            Transform hips = avatar.transform.Find(Hips);
            hips.parent = armature.transform;
            #endregion

            #region SetAnimator
            string AnimationAvatarSource = OutfitGender == OutfitGender.Masculine ? MaleAnimationAvatarName : FemaleAnimationAvatarName;
            Avatar animationAvatar = Resources.Load<Avatar>(AnimationAvatarSource);
            RuntimeAnimatorController animatorController = Resources.Load<RuntimeAnimatorController>(AnimatorControllerName);

            Animator animator = avatar.AddComponent<Animator>();
            animator.runtimeAnimatorController = animatorController;
            animator.avatar = animationAvatar;
            animator.applyRootMotion = true;
            #endregion
        }
    }
}