using UnityEngine;
using System.Collections;

public class drawVerts : MonoBehaviour {
	public Vector3 		translation;
	public Vector3 		eulerAngles;
	public Vector3 		scale = new Vector3(1, 1, 1);
	private MeshFilter 	mf;

	private ParticleSystem 	ps;
	private ParticleSystem.Particle[] particles;
	private Vector3[] origVerts;
	private Vector3[] newVerts;

	void Start() {
		mf = GetComponent<MeshFilter>();
		ps = GetComponent<ParticleSystem> ();
		scale = this.transform.localScale;
		origVerts = mf.sharedMesh.vertices;
		newVerts = new Vector3[origVerts.Length];

		int i = 0;
		while (i < origVerts.Length) {
			ps.Emit(origVerts[i], Vector3.zero, 1f, float.PositiveInfinity, Color.white);
			i++;
		}

		particles = new ParticleSystem.Particle[ps.particleCount];
		ps.GetParticles (particles);
	}
	void Update() {
		origVerts = mf.sharedMesh.vertices;
		Quaternion rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
		Matrix4x4 m = Matrix4x4.TRS(translation, rotation, scale);
		int i = 0;
		while (i < origVerts.Length) {
			newVerts[i] = m.MultiplyPoint3x4(origVerts[i]);
			particles[i].position = newVerts[i];
			i++;
		}
		mf.sharedMesh.vertices = newVerts;
		ps.SetParticles (particles, ps.particleCount);
	}
	
}