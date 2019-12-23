﻿using UnityEngine;
using System.Collections;
using StartApp;

public class ShowAd : StateMachineBehaviour
{

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		int rrr = Random.Range(0, 5);
		if (rrr == 0 || rrr==1)
			ShowInterstitialAds();
	}

	public void ShowInterstitialAds()
	{
		#if UNITY_ANDROID
		StartAppWrapper.showAd();
		StartAppWrapper.loadAd();
		#endif
	}
	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	//	 OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//	{

	//	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}