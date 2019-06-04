﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimatorController : MonoBehaviour {
    private Animator _animator = null;

    [SerializeField] private Avatar _avatarSit      = null;
    [SerializeField] private Avatar _avatarTraverse = null;

    [SerializeField] private RuntimeAnimatorController _controllerSit      = null;
    [SerializeField] private RuntimeAnimatorController _controllerTraverse = null;

    [SerializeField] private GameObject _waypointNetwork_seatToWCBack    = null;
    [SerializeField] private GameObject _waypointNetwork_WCBackToWCFront = null;
    [SerializeField] private GameObject _waypointNetwork_WCFrontToSeat   = null;

    [SerializeField] private bool _isTraversing;
    [SerializeField] private bool _isSitting;

    private NavAgentNoRootMotion _navAgentNoRootMotion;

    private float _navPosZ;

    private float posZ;

    void Start() {
        _animator = GetComponent<Animator>();

        _navPosZ = GetComponent<NavMeshAgent>().transform.position.z;

        posZ = transform.position.z;

        _isTraversing = false;
        _isSitting = true;

        _navAgentNoRootMotion = GetComponent<NavAgentNoRootMotion>();

        _navAgentNoRootMotion.enabled = false;

    }

    void Update() {
        
        //Debug.Log(_navPosZ);

        //AssignTraverseAnimator();
        StartCoroutine(GoToWC());


        AssignSitAnimator();

    }

    IEnumerator GoToWC() {
        if (!_isSitting) {

            

            // set _isTraversing to true to satisfy the condition preparing for traverse
            _isTraversing = true;

            // set the StandingUp parameter in the animator to true to invoke the sit to stand posture
            _animator.SetBool("StandingUp", true);

            // wait for 2 second for the mecanim to complete the sit to stand posture
            yield return new WaitForSeconds(2.2f);

            //float posZ = transform.position.z;
            //posZ = 0.0f;

            //Debug.Log("lalalala");
            _navPosZ = 0.0f;
            //Debug.Log("lalalala");


            AssignTraverseAnimator();
            //_isSitting = true;
        }
    }

    private void AssignTraverseAnimator() {
        if (_isTraversing) {
            _animator.runtimeAnimatorController = _controllerTraverse;
            _animator.avatar                    = _avatarTraverse;

            _navAgentNoRootMotion.enabled = true;
        }
    }

    private void AssignSitAnimator() {
        if (!_isTraversing) {
            _animator.runtimeAnimatorController = _controllerSit;
            _animator.avatar                    = _avatarSit;

            _navAgentNoRootMotion.enabled = false;
        }
    }
}