using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtension
{
    public static class TransformExtensions
    {
        public static void SetPos2D(this Transform trans, Vector3 pos)
        {
            trans.position = new Vector3(pos.x, pos.y, trans.position.z);
        }
        public static void SetPos2D(this Transform trans, float xPos, float yPos)
        {
            trans.position = new Vector3(xPos, yPos, trans.position.z);
        }

        public static void SetLocalPos2D(this Transform trans, float xPos, float yPos)
        {
            trans.localPosition = new Vector3(xPos, yPos, trans.localPosition.z);
        }

        public static void SetLocalPos2D(this Transform trans, Vector2 pos)
        {
            trans.localPosition = new Vector3(pos.x, pos.y, trans.localPosition.z);
        }

        public static void SetLocalPosZ(this Transform trans, float posZ)
        {
            trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, posZ);
        }

        public static void SetLocalPosY(this Transform trans, float posY)
        {
            trans.localPosition = new Vector3(trans.localPosition.x, posY, trans.localPosition.z);
        }

        public static void SetLocalPosX(this Transform trans, float posX)
        {
            trans.localPosition = new Vector3(posX, trans.localPosition.y, trans.localPosition.z);
        }

        public static void SetPosX(this Transform trans, float xPos)
        {
            trans.position = new Vector3(xPos, trans.position.y, trans.position.z);
        }
        public static void SetPosY(this Transform trans, float yPos)
        {
            trans.position = new Vector3(trans.position.x, yPos, trans.position.z);
        }

        /// <summary>
        /// Set world angle
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="angle"></param>
        public static void SetAngleZ(this Transform trans, float angle)
        {
            trans.eulerAngles = new Vector3(trans.eulerAngles.x, trans.eulerAngles.y, angle);
        }

        public static void SetAngleY(this Transform trans, float angle)
        {
            trans.eulerAngles = new Vector3(trans.eulerAngles.x, angle, trans.eulerAngles.z);
        }

        public static void SetAngleX(this Transform trans, float angle)
        {
            trans.eulerAngles = new Vector3(angle, trans.eulerAngles.x, trans.eulerAngles.z);
        }

        public static void SetScaleOneValue(this Transform trans, float value)
        {
            trans.localScale = new Vector3(value, value, trans.localScale.z);
        }
        public static void SetLocalAngleZ(this Transform trans, float angle)
        {
            trans.localEulerAngles = new Vector3(trans.localEulerAngles.x, trans.localEulerAngles.y, angle);
        }

        public static void SetLocalScale2D(this Transform trans, Vector2 scale)
        {
            trans.localScale = new Vector3(scale.x, scale.y, trans.localEulerAngles.z);
        }

        public static void SetLocalScale2D(this Transform trans, float xScale, float yScale)
        {
            trans.localScale = new Vector3(xScale, yScale, trans.localEulerAngles.z);
        }


        //Breadth-first search
        public static Transform FindDeepChild(this Transform aParent, string aName)
        {
            var result = aParent.Find(aName);
            if (result != null)
                return result;
            foreach (Transform child in aParent)
            {
                result = child.FindDeepChild(aName);
                if (result != null)
                    return result;
            }
            return null;
        }
    }
}