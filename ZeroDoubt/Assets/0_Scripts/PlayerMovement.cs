using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    private IMovementInputGetter _movementInputGetter;

    private void Start()
    {
        _movementInputGetter = GetComponent<IMovementInputGetter>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    protected override void Movement()
    {
        var move = _movementInputGetter.GetInput();
        move.Normalize();
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }
}
