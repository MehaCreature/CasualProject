using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MagnetPhysics : MonoBehaviour
{
    public GameObject player;
    public PlayerController controller;
    public Rigidbody rb;
    public string playerCurrentState;

    public GameObject magnet;
    public Vector3 magnetism;
    public float magnetForce;
    public Collider magnetZone;

    private AudioSource audioSource;
    public AudioClip magnetTrapAudio;
    private bool audioPlayed = false;

    void Start()
    {
        player = GameObject.Find("Player");
        controller = player.GetComponent<PlayerController>();
        rb = player.GetComponent<Rigidbody>();
        magnetZone = gameObject.GetComponent<SphereCollider>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        playerCurrentState = player.GetComponentInChildren<PlayerController>().currentStateString;
        magnetism = (transform.position - player.transform.position) * magnetForce * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if(controller.isMagnetized == true)
        {
            rb.AddForce(magnetism, ForceMode.VelocityChange);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player && playerCurrentState == "Solid")
        {
            controller.isMagnetized = true;
            //rb.AddForce(magnetism, ForceMode.VelocityChange);

            if (controller.isMagnetized == true && audioPlayed == false)
            {
                audioSource.PlayOneShot(magnetTrapAudio, 1.0F);
                audioPlayed = true;
            }
        }
        else
        {
            controller.isMagnetized = false;
            audioPlayed = false;
        }
    }
}
