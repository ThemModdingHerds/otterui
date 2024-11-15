// https://github.com/ppiecuch/OtterUI/blob/master/API/inc/Common/Types.h

struct ControlLayout
{
    char mFourCC[4];
    u32 mDataSize;

    float mCenterX;
    float mCenterY;

    float mX;
    float mY;

    float mWidth;
    float mHeight;

    float mRotation;
}; // 36 bytes

struct ControlData
{
    char mFourCC[4];
    u32 mDataSize;

    u32 mID;
    char mName[64];
    float mPosition[2];
    float mCenter[2];
    float mSize[2];
    float mRotation;

    u32 mAnchorFlags;
    s32 mMaskID;
}; // 112 bytes

struct KeyFrameData
{
    char mFourCC[4];
    u32 mDataSize;

    u32 mFrame;

    s32 mEaseType;
    s32 mEaseAmount;

    float mLeftAbs;
    float mLeftRel;

    float mRightAbs;
    float mRightRel;

    float mTopAbs;
    float mTopRel;

    float mBottomAbs;
    float mBottomRel;
}; // 52

// https://github.com/ppiecuch/OtterUI/blob/master/API/src/Data/OtterData.h

struct ActionData
{
    char mFourCC[4];
    u32 mDataSize;
}; // 8

// MSGA
struct MessageActionData : ActionData
{
    u32 mMessageID;
}; // 12

// SNDA
struct SoundActionData : ActionData
{
    u32 mSoundID;
    float mVolume;
}; // 16

// SPLT
struct SpriteLayout : ControlLayout
{
    u32 mColor;
    float mSkew;
}; // 44

// GSPR
struct SpriteData : ControlData
{
    s32 mTextureID;
    u32 mColor;
    float mSkew;
    u32 mFlipType;
    u32 mNumControls;
    ControlData mBuffer[mNumControls];
}; // 132

// LBLT
struct LabelLayout : ControlLayout
{
    u32 mColor;
    float mScaleX;
    float mScaleY;
    float mSkew;
    float mDropShadow;
}; // 48

// GLBL
struct LabelData : ControlData
{
    u32 mFontID;
    u32 mColor;
    float mScaleX;
    float mScaleY;
    float mSkew;
    s32 mDropShadow;
    u32 mHAlign;
    u32 mVAlign;
    float mLeading;
    s32 mTracking;
    u32 mTextFit;
    u32 mTextBufferSize;
    char mBuffer[mTextBufferSize];
};

// BTLT
struct ButtonLayout : ControlLayout
{

}; // 36

// GBTT
struct ButtonData : ControlData
{
    u32 mDefaultTextureID;
    u32 mDownTextureID;

    u32 mFontID;
    u32 mDefaultColor;
    u32 mDownColor;
    float mScaleX;
    float mScaleY;
    u32 mHAlign;
    u32 mVAlign;

    u32 mNumOnClickActions;
    u32 mTextBufferSize;
    char mBuffer[mTextBufferSize];
    ActionData mBuffer2[mNumOnClickActions];
}; // 156

// GPLT
struct GroupLayout : ControlLayout
{
    u32 mColor;
};

// GGRP
struct GroupData : ControlData
{
    u32 mNumControls;
    ControlData mBuffer[mNumControls];
};

// TBLT
struct TableLayout : ControlLayout
{

}; // 36

// GTBL
struct TableData : ControlData
{
    u32 mDefaultRowHeight;
    u32 mRowSpacing;
};

// TGLT
struct ToggleLayout : ControlLayout
{
    u32 mColor;
};

// GTGL
struct ToggleData : ControlData
{
    s32 mOnTextureID;
    s32 mOffTextureID;
    u32 mColor;
};

// SLLT
struct SliderLayout : ControlLayout
{
    u32 mColor;
};

// GSLD
struct SliderData : ControlData
{
    u32 mThumbWidth;
    u32 mThumbHeight;

    s32 mStartTextureID;
    s32 mMiddleTextureID;
    s32 mEndTextureID;
    s32 mThumbTextureID;

    s32 mMin;
    s32 mMax;
    s32 mStep;
    s32 mValue;

    u32 mColor;
};

// SPLT
struct MaskLayout : ControlLayout
{
    float mSkew;
};

// GMSK
struct MaskData : ControlData
{
    s32 mTextureID;
    float mSkew;
    u32 mFlipType;
};

// GGAC
struct AnimationChannelData
{
    char mFourCC[4];
    u32 mDataSize;

    s32 mControlID;
    u32 mNumKeyFrames;
    KeyFrameData mBuffer[mNumKeyFrames];
};

// MCFR
struct MainChannelFrameData
{
    char mFourCC[4];
    u32 mDataSize;
    char mName[64];
    u32 mFrame;
    u32 mNumActions;
    ActionData mBuffer[mNumActions];
};

// GGAN
struct AnimationData
{
    char mFourCC[4];
    u32 mDataSize;

    char mName[64];

    u32 mNumFrames;
    s32 mRepeatStart;
    s32 mRepeatEnd;

    u32 mNumAnimationChannels;
    u32 mNumMainChannelFrames;
    
    u32 mMainChannelFramesOffset;

    u64 _offset = $;

    AnimationChannelData mBuffer[mNumAnimationChannels];
    MainChannelFrameData mBuffer2[mNumMainChannelFrames] @ _offset + mMainChannelFramesOffset;
};

// GGAL
struct AnimationListData
{
    char mFourCC[4];
    u32 mDataSize;

    u32 mNumAnimations;
    AnimationData mBuffer[mNumAnimations];

};

// GGVW
struct ViewData : ControlData
{
    u32 mNumTextures;
    u32 mNumSounds;
    u32 mNumControls;

    u32 mTexturesOffset;
    u32 mSoundsOffset;
    u32 mControlsOffset;
    u32 mAnimationListOffset;

    u64 _offset = $;

    AnimationListData mBuffer @ _offset + mAnimationListOffset;
    // Texture IDs
    s32 mBuffer2[mNumTextures] @ _offset + mTexturesOffset;
    // Sound IDs
    s32 mBuffer3[mNumSounds] @ _offset + mSoundsOffset;
    ControlData mBuffer4[mNumControls] @ _offset + mControlsOffset;
};

struct TextureRect
{
    u32 mTextureID;
    float uv[4];
};

struct TextureData
{
    u32 mTextureID;
    char mTextureName[256];
    s32 mRefCount;
    TextureRect mTextureRect;
};

struct SoundData
{
    u32 mSoundID;
    char mSoundName[256];
    s32 mRefCount;
};

struct GlyphData
{
    u32 mCharCode;
    u32 mIsImageGlypth;

    u32 mX;
    u32 mY;
    u32 mWidth;
    u32 mHeight;

    u32 mTop;
    u32 mAdvance;
    u32 mLeftBearing;

    u32 mAtlasIndex;
};

struct MessageData
{
    u32 mID;
    char mText[64];
};

// GFNT
struct FontData
{
    char mFourCC[4];
    u32 mDataSize;

    u32 mID;
    char mName[64];
    u32 mFontSize;
    u32 mFontWidth;
    u32 mFontHeight;

    u32 mMaxTop;
    u32 mNumTextures;
    u32 mNumGlyphs;

    GlyphData mBuffer[mNumGlyphs];
};

// CSGG
struct SceneData
{
    char mFourCC[4];
    u32 mDataSize;
    float mVersion;
    u32 mID;

    u32 mNumFonts;
    u32 mNumTextures;
    u32 mNumSounds;
    u32 mNumViews;
    u32 mNumMessages;

    u32 mFontsOffset;
    u32 mTexturesOffset;
    u32 mSoundsOffset;
    u32 mViewsOffset;
    u32 mMessageOffset;

    u64 _offset = $;

    FontData mBuffer[mNumFonts] @ _offset + mFontsOffset;
    TextureData mBuffer2[mNumTextures] @ _offset + mTexturesOffset;
    SoundData mBuffer3[mNumSounds] @ _offset + mSoundsOffset;
    ViewData mBuffer4[mNumViews] @ _offset + mViewsOffset;
    MessageData mBuffer5[mNumViews] @ _offset + mMessageOffset;
};

SceneData scene @ $;