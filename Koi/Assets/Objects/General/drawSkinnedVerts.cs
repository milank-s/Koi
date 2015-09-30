using UnityEngine;
using System.Collections;

public class drawSkinnedVerts : MonoBehaviour {
	public Vector3 		translation;
	public Vector3 		eulerAngles;
	public Vector3 		scale = new Vector3(1, 1, 1);
	public float size;
	private SkinnedMeshRenderer 	mf;

	private ParticleSystem 	ps;
	private ParticleSystem.Particle[] particles;
	private Vector3[] origVerts;
	private Vector3[] newVerts;

	void Start() {
		mf = GetComponent<SkinnedMeshRenderer>();
		ps = GetComponent<ParticleSystem> ();
		origVerts = mf.sharedMesh.vertices;

		int i = 0;
		while (i < origVerts.Length) {
			ps.Emit(origVerts[i], Vector3.zero, size, float.PositiveInfinity, Color.white);
			i++;
		}

		particles = new ParticleSystem.Particle[ps.particleCount];
		ps.GetParticles (particles);
	}
	void Update() {
		Mesh snapshot  = new Mesh();
		mf.BakeMesh (snapshot);
		origVerts = snapshot.vertices;
		int i = 0;
		while (i < origVerts.Length) {
			particles[i].position = origVerts[i];
			i++;
		}
		ps.SetParticles (particles, ps.particleCount);
	}
	
}