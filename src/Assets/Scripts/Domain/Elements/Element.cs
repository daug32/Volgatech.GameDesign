﻿using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Domain.Elements
{
    internal class Element
    {
        public readonly string Guid;

        public readonly ElementId Id;
        public readonly string AssetsPath;

        private readonly Sprite _sprite;

        public Element( Sprite sprite )
        {
            string[] nameParts = sprite.name.Split( '_' );
            Id = new ElementId( nameParts.Last() );

            _sprite = sprite;
        }

        public override string ToString() => $"Element (id: ${Guid}, path: ${AssetsPath})";

        public string BuildName() => $"element_{Id}";

        public GameObject CreateGameObject()
        {
            var elementGameObject = new GameObject();

            var elementUiImage = elementGameObject.AddComponent<Image>();
            elementGameObject.name = BuildName();
            elementUiImage.preserveAspect = true;
            elementUiImage.sprite = _sprite;
            elementGameObject.SetActive( true );

            return elementGameObject;
        }
    }
}
