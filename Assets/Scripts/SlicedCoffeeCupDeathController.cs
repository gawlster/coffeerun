using UnityEngine;

public class SlicedCoffeeCupDeathController: MonoBehaviour {
    private void Start() {
        var i = 0;
        var velocity = UnityEngine.Random.Range(5f, 10f);
        while (true) {
            try {
                var child = transform.GetChild(i).gameObject;
                var rigidbody = child.GetComponent<Rigidbody>();
                velocity += UnityEngine.Random.Range(5f, 7.5f);
                rigidbody.velocity = new Vector3(0, 0, velocity);
                i++;
            } catch {
                break;
            }
        }
    }
}