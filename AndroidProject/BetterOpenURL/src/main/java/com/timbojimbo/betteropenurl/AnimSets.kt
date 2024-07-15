package com.timbojimbo.betteropenurl

internal class AnimSet(val firstAnim: Int, val secondAnim: Int)

internal class CustomTabAnimSets
{
    companion object {
        val pullIn = AnimSet(R.anim.pull_from_back, R.anim.push_to_back)
        val fallIn = AnimSet(R.anim.push_from_front, R.anim.pull_to_front)
        val openFromBottom = AnimSet(R.anim.open_from_bottom, R.anim.close_to_bottom)

        fun get(enumVal: CustomTabAnimations) : AnimSet
        {
            return when (enumVal) {
                CustomTabAnimations.pullIn -> pullIn
                CustomTabAnimations.fallIn -> fallIn
                CustomTabAnimations.openFromBottom -> openFromBottom
            }
        }
    }
}

internal class ApplicationAnimSets
{
    companion object {
        val fallAway = AnimSet(R.anim.push_to_back, R.anim.pull_from_back)
        val flyAway = AnimSet(R.anim.pull_to_front, R.anim.push_from_front)
        val closeToBottom = AnimSet(R.anim.close_to_bottom, R.anim.open_from_bottom)
        var crouchDown = AnimSet(R.anim.crouch_down, R.anim.stand_up)

        fun get(enumVal: ApplicationAnimation) : AnimSet
        {
            return when (enumVal) {
                ApplicationAnimation.fallAway -> fallAway
                ApplicationAnimation.flyAway -> flyAway
                ApplicationAnimation.closeToBottom -> closeToBottom
                ApplicationAnimation.crouchDown -> crouchDown
            }
        }
    }
}
