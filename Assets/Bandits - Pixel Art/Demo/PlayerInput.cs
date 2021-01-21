public class PlayerInput : ICharacterInput {
    public float GetHorizontalMove() {
        return Input.GetAxis("Horizontal");
    }

    public bool Attack() {
        return Input.GetMouseButtonDown(0);
    }

    public bool Jump() {
        return Input.GetKeyDown("space");
    }

    public bool Block() {
        return Input.GetMouseButtonDown(1);
    }
}

public class EnemyInput : ICharacterInput {
    public float horizontalMove;
    public bool isAttacking;

    public float GetHorizontalMove() {
        return horizontalMove;
    }

    public bool Attack() {
        return isAttacking;
    }

    public bool Jump() {
        return false;
    }

    public bool Block() {
        return false;
    }
}