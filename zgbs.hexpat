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
};

// MCFR
struct MainChannelFrameData
{
    char mFourCC[4];
    u32 mDataSize;
    char mName[64];
    u32 mFrame;
    u32 mNumActions;
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
};

// GGAL
struct AnimationListData
{
    char mFourCC[4];
    u32 mDataSize;

    u32 mNumAnimations;

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
};

// Z-Engine games

// GSPP
struct GSPP : ControlData
{
    u32 mTextureID;
    padding[36];
    u32 mFooter;
};

// SPPL
struct SPPL : ControlLayout
{
    padding[24];
    u32 mFooter;
};

// ARCC
struct ARCC : ControlData
{
    u32 mUnknown1;
    u32 mUnknown2;
    u32 mUnknown3;
    u32 mUnknown4;
    u32 mUnknown5;
    u32 mUnknown6;
    u32 mFlag;
};

// ARLT
struct ARLT : ControlLayout
{
    u32 mUnknown1;
    u32 mUnknown2;
    u32 mUnknown3;
    u32 mUnknown4;
    u32 mUnknown5;
};

SceneData scene @ $;