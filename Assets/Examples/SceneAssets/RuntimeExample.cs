﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RuntimeExample : MonoBehaviour
{
    [SerializeField]
    TextureCurve _curve = null;

    [SerializeField]
    Material _material = null;

    Material _preview = null;

    [SerializeField]
    [Range (0, 10)]
    float _speed = 1.0f;

    SpriteRenderer _renderer = null;

    void Start()
    {
        if (_curve == null)
            return;
        _curve.Bake();

        _preview = new Material(Shader.Find("Hidden/Preview"));
        _preview.hideFlags = HideFlags.DontSave;

        _renderer = GetComponent<SpriteRenderer>();
        _renderer.material = _preview;
    }

    void Update()
    {
        if (_material == null)
            return;

        if (_renderer.sprite.texture != _curve.Texture)
        {
            _renderer.sprite = Sprite.Create(
               _curve.Texture,
               new Rect(0, 0, _curve.Resolution, 1),
               Vector2.zero,
               100
           );
        }

        _preview.SetFloat("_Speed", _speed);

        _material.SetTexture("_CurveTex", _curve.Texture);
        _material.SetFloat("_Speed", _speed);
    }
}