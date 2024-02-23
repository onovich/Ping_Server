namespace Ping.Server {

    public enum KeyCode {
        None = 0,
        Backspace = 8,
        Tab = 9,
        Clear = 12,
        Return = 13,
        Escape = 27,
        Space = 32,
        Alpha0 = 48,
        Alpha1 = 49,
        Alpha2 = 50,
        Alpha3 = 51,
        Alpha4 = 52,
        Alpha5 = 53,
        Alpha6 = 54,
        Alpha7 = 55,
        Alpha8 = 56,
        Alpha9 = 57,
        A = 97,
        B = 98,
        C = 99,
        D = 100,
        E = 101,
        F = 102,
        G = 103,
        H = 104,
        I = 105,
        J = 106,
        K = 107,
        L = 108,
        M = 109,
        N = 110,
        O = 111,
        P = 112,
        Q = 113,
        R = 114,
        S = 115,
        T = 116,
        U = 117,
        V = 118,
        //
        // 摘要:
        //     'w' key.
        W = 119,
        //
        // 摘要:
        //     'x' key.
        X = 120,
        //
        // 摘要:
        //     'y' key.
        Y = 121,
        //
        // 摘要:
        //     'z' key.
        Z = 122,
        //
        // 摘要:
        //     Left curly bracket key '{'. Deprecated if "Use Physical Keys" is enabled in instead.
        LeftCurlyBracket = 123,
        //
        // 摘要:
        //     Pipe '|' key. Deprecated if "Use Physical Keys" is enabled in instead.
        Pipe = 124,
        //
        // 摘要:
        //     Right curly bracket key '}'. Deprecated if "Use Physical Keys" is enabled in
        //     instead.
        RightCurlyBracket = 125,
        //
        // 摘要:
        //     Tilde '~' key. Deprecated if "Use Physical Keys" is enabled in instead.
        Tilde = 126,
        //
        // 摘要:
        //     The forward delete key.
        Delete = 127,
        //
        // 摘要:
        //     Numeric keypad 0.
        Keypad0 = 256,
        //
        // 摘要:
        //     Numeric keypad 1.
        Keypad1 = 257,
        //
        // 摘要:
        //     Numeric keypad 2.
        Keypad2 = 258,
        //
        // 摘要:
        //     Numeric keypad 3.
        Keypad3 = 259,
        //
        // 摘要:
        //     Numeric keypad 4.
        Keypad4 = 260,
        //
        // 摘要:
        //     Numeric keypad 5.
        Keypad5 = 261,
        //
        // 摘要:
        //     Numeric keypad 6.
        Keypad6 = 262,
        //
        // 摘要:
        //     Numeric keypad 7.
        Keypad7 = 263,
        //
        // 摘要:
        //     Numeric keypad 8.
        Keypad8 = 264,
        //
        // 摘要:
        //     Numeric keypad 9.
        Keypad9 = 265,
        //
        // 摘要:
        //     Numeric keypad '.'.
        KeypadPeriod = 266,
        //
        // 摘要:
        //     Numeric keypad '/'.
        KeypadDivide = 267,
        //
        // 摘要:
        //     Numeric keypad '*'.
        KeypadMultiply = 268,
        //
        // 摘要:
        //     Numeric keypad '-'.
        KeypadMinus = 269,
        //
        // 摘要:
        //     Numeric keypad '+'.
        KeypadPlus = 270,
        //
        // 摘要:
        //     Numeric keypad Enter.
        KeypadEnter = 271,
        //
        // 摘要:
        //     Numeric keypad '='.
        KeypadEquals = 272,
        //
        // 摘要:
        //     Up arrow key.
        UpArrow = 273,
        //
        // 摘要:
        //     Down arrow key.
        DownArrow = 274,
        //
        // 摘要:
        //     Right arrow key.
        RightArrow = 275,
        //
        // 摘要:
        //     Left arrow key.
        LeftArrow = 276,
        //
        // 摘要:
        //     Insert key key.
        Insert = 277,
        //
        // 摘要:
        //     Home key.
        Home = 278,
        //
        // 摘要:
        //     End key.
        End = 279,
        //
        // 摘要:
        //     Page up.
        PageUp = 280,
        //
        // 摘要:
        //     Page down.
        PageDown = 281,
        //
        // 摘要:
        //     F1 function key.
        F1 = 282,
        //
        // 摘要:
        //     F2 function key.
        F2 = 283,
        //
        // 摘要:
        //     F3 function key.
        F3 = 284,
        //
        // 摘要:
        //     F4 function key.
        F4 = 285,
        //
        // 摘要:
        //     F5 function key.
        F5 = 286,
        //
        // 摘要:
        //     F6 function key.
        F6 = 287,
        //
        // 摘要:
        //     F7 function key.
        F7 = 288,
        //
        // 摘要:
        //     F8 function key.
        F8 = 289,
        //
        // 摘要:
        //     F9 function key.
        F9 = 290,
        //
        // 摘要:
        //     F10 function key.
        F10 = 291,
        //
        // 摘要:
        //     F11 function key.
        F11 = 292,
        //
        // 摘要:
        //     F12 function key.
        F12 = 293,
        //
        // 摘要:
        //     F13 function key.
        F13 = 294,
        //
        // 摘要:
        //     F14 function key.
        F14 = 295,
        //
        // 摘要:
        //     F15 function key.
        F15 = 296,
        //
        // 摘要:
        //     Numlock key.
        Numlock = 300,
        //
        // 摘要:
        //     Capslock key.
        CapsLock = 301,
        //
        // 摘要:
        //     Scroll lock key.
        ScrollLock = 302,
        //
        // 摘要:
        //     Right shift key.
        RightShift = 303,
        //
        // 摘要:
        //     Left shift key.
        LeftShift = 304,
        //
        // 摘要:
        //     Right Control key.
        RightControl = 305,
        //
        // 摘要:
        //     Left Control key.
        LeftControl = 306,
        //
        // 摘要:
        //     Right Alt key.
        RightAlt = 307,
        //
        // 摘要:
        //     Left Alt key.
        LeftAlt = 308,
        //
        // 摘要:
        //     Maps to right Windows key or right Command key if physical keys are enabled in
        //     Input Manager settings, otherwise maps to right Command key only.
        RightMeta = 309,
        //
        // 摘要:
        //     Right Command key.
        RightCommand = 309,
        //
        // 摘要:
        //     Right Command key.
        RightApple = 309,
        //
        // 摘要:
        //     Maps to left Windows key or left Command key if physical keys are enabled in
        //     Input Manager settings, otherwise maps to left Command key only.
        LeftMeta = 310,
        //
        // 摘要:
        //     Left Command key.
        LeftCommand = 310,
        //
        // 摘要:
        //     Left Command key.
        LeftApple = 310,
        //
        // 摘要:
        //     Left Windows key. Deprecated if "Use Physical Keys" is enabled in instead.
        LeftWindows = 311,
        //
        // 摘要:
        //     Right Windows key. Deprecated if "Use Physical Keys" is enabled in instead.
        RightWindows = 312,
        //
        // 摘要:
        //     Alt Gr key. Deprecated if "Use Physical Keys" is enabled in instead.
        AltGr = 313,
        //
        // 摘要:
        //     Help key. Deprecated if "Use Physical Keys" is enabled in, doesn't map to any
        //     physical key.
        Help = 315,
        //
        // 摘要:
        //     Print key.
        Print = 316,
        //
        // 摘要:
        //     Sys Req key. Deprecated if "Use Physical Keys" is enabled in, doesn't map to
        //     any physical key.
        SysReq = 317,
        //
        // 摘要:
        //     Break key. Deprecated if "Use Physical Keys" is enabled in, doesn't map to any
        //     physical key.
        Break = 318,
        //
        // 摘要:
        //     Menu key.
        Menu = 319,
        //
        // 摘要:
        //     The Left (or primary) mouse button.
        Mouse0 = 323,
        //
        // 摘要:
        //     Right mouse button (or secondary mouse button).
        Mouse1 = 324,
        //
        // 摘要:
        //     Middle mouse button (or third button).
        Mouse2 = 325,
        //
        // 摘要:
        //     Additional (fourth) mouse button.
        Mouse3 = 326,
        //
        // 摘要:
        //     Additional (fifth) mouse button.
        Mouse4 = 327,
        //
        // 摘要:
        //     Additional (or sixth) mouse button.
        Mouse5 = 328,
        //
        // 摘要:
        //     Additional (or seventh) mouse button.
        Mouse6 = 329,
        //
        // 摘要:
        //     Button 0 on any joystick.
        JoystickButton0 = 330,
        //
        // 摘要:
        //     Button 1 on any joystick.
        JoystickButton1 = 331,
        //
        // 摘要:
        //     Button 2 on any joystick.
        JoystickButton2 = 332,
        //
        // 摘要:
        //     Button 3 on any joystick.
        JoystickButton3 = 333,
        //
        // 摘要:
        //     Button 4 on any joystick.
        JoystickButton4 = 334,
        //
        // 摘要:
        //     Button 5 on any joystick.
        JoystickButton5 = 335,
        //
        // 摘要:
        //     Button 6 on any joystick.
        JoystickButton6 = 336,
        //
        // 摘要:
        //     Button 7 on any joystick.
        JoystickButton7 = 337,
        //
        // 摘要:
        //     Button 8 on any joystick.
        JoystickButton8 = 338,
        //
        // 摘要:
        //     Button 9 on any joystick.
        JoystickButton9 = 339,
        //
        // 摘要:
        //     Button 10 on any joystick.
        JoystickButton10 = 340,
        //
        // 摘要:
        //     Button 11 on any joystick.
        JoystickButton11 = 341,
        //
        // 摘要:
        //     Button 12 on any joystick.
        JoystickButton12 = 342,
        //
        // 摘要:
        //     Button 13 on any joystick.
        JoystickButton13 = 343,
        //
        // 摘要:
        //     Button 14 on any joystick.
        JoystickButton14 = 344,
        //
        // 摘要:
        //     Button 15 on any joystick.
        JoystickButton15 = 345,
        //
        // 摘要:
        //     Button 16 on any joystick.
        JoystickButton16 = 346,
        //
        // 摘要:
        //     Button 17 on any joystick.
        JoystickButton17 = 347,
        //
        // 摘要:
        //     Button 18 on any joystick.
        JoystickButton18 = 348,
        //
        // 摘要:
        //     Button 19 on any joystick.
        JoystickButton19 = 349,
        //
        // 摘要:
        //     Button 0 on first joystick.
        Joystick1Button0 = 350,
        //
        // 摘要:
        //     Button 1 on first joystick.
        Joystick1Button1 = 351,
        //
        // 摘要:
        //     Button 2 on first joystick.
        Joystick1Button2 = 352,
        //
        // 摘要:
        //     Button 3 on first joystick.
        Joystick1Button3 = 353,
        //
        // 摘要:
        //     Button 4 on first joystick.
        Joystick1Button4 = 354,
        //
        // 摘要:
        //     Button 5 on first joystick.
        Joystick1Button5 = 355,
        //
        // 摘要:
        //     Button 6 on first joystick.
        Joystick1Button6 = 356,
        //
        // 摘要:
        //     Button 7 on first joystick.
        Joystick1Button7 = 357,
        //
        // 摘要:
        //     Button 8 on first joystick.
        Joystick1Button8 = 358,
        //
        // 摘要:
        //     Button 9 on first joystick.
        Joystick1Button9 = 359,
        //
        // 摘要:
        //     Button 10 on first joystick.
        Joystick1Button10 = 360,
        //
        // 摘要:
        //     Button 11 on first joystick.
        Joystick1Button11 = 361,
        //
        // 摘要:
        //     Button 12 on first joystick.
        Joystick1Button12 = 362,
        //
        // 摘要:
        //     Button 13 on first joystick.
        Joystick1Button13 = 363,
        //
        // 摘要:
        //     Button 14 on first joystick.
        Joystick1Button14 = 364,
        //
        // 摘要:
        //     Button 15 on first joystick.
        Joystick1Button15 = 365,
        //
        // 摘要:
        //     Button 16 on first joystick.
        Joystick1Button16 = 366,
        //
        // 摘要:
        //     Button 17 on first joystick.
        Joystick1Button17 = 367,
        //
        // 摘要:
        //     Button 18 on first joystick.
        Joystick1Button18 = 368,
        //
        // 摘要:
        //     Button 19 on first joystick.
        Joystick1Button19 = 369,
        //
        // 摘要:
        //     Button 0 on second joystick.
        Joystick2Button0 = 370,
        //
        // 摘要:
        //     Button 1 on second joystick.
        Joystick2Button1 = 371,
        //
        // 摘要:
        //     Button 2 on second joystick.
        Joystick2Button2 = 372,
        //
        // 摘要:
        //     Button 3 on second joystick.
        Joystick2Button3 = 373,
        //
        // 摘要:
        //     Button 4 on second joystick.
        Joystick2Button4 = 374,
        //
        // 摘要:
        //     Button 5 on second joystick.
        Joystick2Button5 = 375,
        //
        // 摘要:
        //     Button 6 on second joystick.
        Joystick2Button6 = 376,
        //
        // 摘要:
        //     Button 7 on second joystick.
        Joystick2Button7 = 377,
        //
        // 摘要:
        //     Button 8 on second joystick.
        Joystick2Button8 = 378,
        //
        // 摘要:
        //     Button 9 on second joystick.
        Joystick2Button9 = 379,
        //
        // 摘要:
        //     Button 10 on second joystick.
        Joystick2Button10 = 380,
        //
        // 摘要:
        //     Button 11 on second joystick.
        Joystick2Button11 = 381,
        //
        // 摘要:
        //     Button 12 on second joystick.
        Joystick2Button12 = 382,
        //
        // 摘要:
        //     Button 13 on second joystick.
        Joystick2Button13 = 383,
        //
        // 摘要:
        //     Button 14 on second joystick.
        Joystick2Button14 = 384,
        //
        // 摘要:
        //     Button 15 on second joystick.
        Joystick2Button15 = 385,
        //
        // 摘要:
        //     Button 16 on second joystick.
        Joystick2Button16 = 386,
        //
        // 摘要:
        //     Button 17 on second joystick.
        Joystick2Button17 = 387,
        //
        // 摘要:
        //     Button 18 on second joystick.
        Joystick2Button18 = 388,
        //
        // 摘要:
        //     Button 19 on second joystick.
        Joystick2Button19 = 389,
        //
        // 摘要:
        //     Button 0 on third joystick.
        Joystick3Button0 = 390,
        //
        // 摘要:
        //     Button 1 on third joystick.
        Joystick3Button1 = 391,
        //
        // 摘要:
        //     Button 2 on third joystick.
        Joystick3Button2 = 392,
        //
        // 摘要:
        //     Button 3 on third joystick.
        Joystick3Button3 = 393,
        //
        // 摘要:
        //     Button 4 on third joystick.
        Joystick3Button4 = 394,
        //
        // 摘要:
        //     Button 5 on third joystick.
        Joystick3Button5 = 395,
        //
        // 摘要:
        //     Button 6 on third joystick.
        Joystick3Button6 = 396,
        //
        // 摘要:
        //     Button 7 on third joystick.
        Joystick3Button7 = 397,
        //
        // 摘要:
        //     Button 8 on third joystick.
        Joystick3Button8 = 398,
        //
        // 摘要:
        //     Button 9 on third joystick.
        Joystick3Button9 = 399,
        //
        // 摘要:
        //     Button 10 on third joystick.
        Joystick3Button10 = 400,
        //
        // 摘要:
        //     Button 11 on third joystick.
        Joystick3Button11 = 401,
        //
        // 摘要:
        //     Button 12 on third joystick.
        Joystick3Button12 = 402,
        //
        // 摘要:
        //     Button 13 on third joystick.
        Joystick3Button13 = 403,
        //
        // 摘要:
        //     Button 14 on third joystick.
        Joystick3Button14 = 404,
        //
        // 摘要:
        //     Button 15 on third joystick.
        Joystick3Button15 = 405,
        //
        // 摘要:
        //     Button 16 on third joystick.
        Joystick3Button16 = 406,
        //
        // 摘要:
        //     Button 17 on third joystick.
        Joystick3Button17 = 407,
        //
        // 摘要:
        //     Button 18 on third joystick.
        Joystick3Button18 = 408,
        //
        // 摘要:
        //     Button 19 on third joystick.
        Joystick3Button19 = 409,
        //
        // 摘要:
        //     Button 0 on forth joystick.
        Joystick4Button0 = 410,
        //
        // 摘要:
        //     Button 1 on forth joystick.
        Joystick4Button1 = 411,
        //
        // 摘要:
        //     Button 2 on forth joystick.
        Joystick4Button2 = 412,
        //
        // 摘要:
        //     Button 3 on forth joystick.
        Joystick4Button3 = 413,
        //
        // 摘要:
        //     Button 4 on forth joystick.
        Joystick4Button4 = 414,
        //
        // 摘要:
        //     Button 5 on forth joystick.
        Joystick4Button5 = 415,
        //
        // 摘要:
        //     Button 6 on forth joystick.
        Joystick4Button6 = 416,
        //
        // 摘要:
        //     Button 7 on forth joystick.
        Joystick4Button7 = 417,
        //
        // 摘要:
        //     Button 8 on forth joystick.
        Joystick4Button8 = 418,
        //
        // 摘要:
        //     Button 9 on forth joystick.
        Joystick4Button9 = 419,
        //
        // 摘要:
        //     Button 10 on forth joystick.
        Joystick4Button10 = 420,
        //
        // 摘要:
        //     Button 11 on forth joystick.
        Joystick4Button11 = 421,
        //
        // 摘要:
        //     Button 12 on forth joystick.
        Joystick4Button12 = 422,
        //
        // 摘要:
        //     Button 13 on forth joystick.
        Joystick4Button13 = 423,
        //
        // 摘要:
        //     Button 14 on forth joystick.
        Joystick4Button14 = 424,
        //
        // 摘要:
        //     Button 15 on forth joystick.
        Joystick4Button15 = 425,
        //
        // 摘要:
        //     Button 16 on forth joystick.
        Joystick4Button16 = 426,
        //
        // 摘要:
        //     Button 17 on forth joystick.
        Joystick4Button17 = 427,
        //
        // 摘要:
        //     Button 18 on forth joystick.
        Joystick4Button18 = 428,
        //
        // 摘要:
        //     Button 19 on forth joystick.
        Joystick4Button19 = 429,
        //
        // 摘要:
        //     Button 0 on fifth joystick.
        Joystick5Button0 = 430,
        //
        // 摘要:
        //     Button 1 on fifth joystick.
        Joystick5Button1 = 431,
        //
        // 摘要:
        //     Button 2 on fifth joystick.
        Joystick5Button2 = 432,
        //
        // 摘要:
        //     Button 3 on fifth joystick.
        Joystick5Button3 = 433,
        //
        // 摘要:
        //     Button 4 on fifth joystick.
        Joystick5Button4 = 434,
        //
        // 摘要:
        //     Button 5 on fifth joystick.
        Joystick5Button5 = 435,
        //
        // 摘要:
        //     Button 6 on fifth joystick.
        Joystick5Button6 = 436,
        //
        // 摘要:
        //     Button 7 on fifth joystick.
        Joystick5Button7 = 437,
        //
        // 摘要:
        //     Button 8 on fifth joystick.
        Joystick5Button8 = 438,
        //
        // 摘要:
        //     Button 9 on fifth joystick.
        Joystick5Button9 = 439,
        //
        // 摘要:
        //     Button 10 on fifth joystick.
        Joystick5Button10 = 440,
        //
        // 摘要:
        //     Button 11 on fifth joystick.
        Joystick5Button11 = 441,
        //
        // 摘要:
        //     Button 12 on fifth joystick.
        Joystick5Button12 = 442,
        //
        // 摘要:
        //     Button 13 on fifth joystick.
        Joystick5Button13 = 443,
        //
        // 摘要:
        //     Button 14 on fifth joystick.
        Joystick5Button14 = 444,
        //
        // 摘要:
        //     Button 15 on fifth joystick.
        Joystick5Button15 = 445,
        //
        // 摘要:
        //     Button 16 on fifth joystick.
        Joystick5Button16 = 446,
        //
        // 摘要:
        //     Button 17 on fifth joystick.
        Joystick5Button17 = 447,
        //
        // 摘要:
        //     Button 18 on fifth joystick.
        Joystick5Button18 = 448,
        //
        // 摘要:
        //     Button 19 on fifth joystick.
        Joystick5Button19 = 449,
        //
        // 摘要:
        //     Button 0 on sixth joystick.
        Joystick6Button0 = 450,
        //
        // 摘要:
        //     Button 1 on sixth joystick.
        Joystick6Button1 = 451,
        //
        // 摘要:
        //     Button 2 on sixth joystick.
        Joystick6Button2 = 452,
        //
        // 摘要:
        //     Button 3 on sixth joystick.
        Joystick6Button3 = 453,
        //
        // 摘要:
        //     Button 4 on sixth joystick.
        Joystick6Button4 = 454,
        //
        // 摘要:
        //     Button 5 on sixth joystick.
        Joystick6Button5 = 455,
        //
        // 摘要:
        //     Button 6 on sixth joystick.
        Joystick6Button6 = 456,
        //
        // 摘要:
        //     Button 7 on sixth joystick.
        Joystick6Button7 = 457,
        //
        // 摘要:
        //     Button 8 on sixth joystick.
        Joystick6Button8 = 458,
        //
        // 摘要:
        //     Button 9 on sixth joystick.
        Joystick6Button9 = 459,
        //
        // 摘要:
        //     Button 10 on sixth joystick.
        Joystick6Button10 = 460,
        //
        // 摘要:
        //     Button 11 on sixth joystick.
        Joystick6Button11 = 461,
        //
        // 摘要:
        //     Button 12 on sixth joystick.
        Joystick6Button12 = 462,
        //
        // 摘要:
        //     Button 13 on sixth joystick.
        Joystick6Button13 = 463,
        //
        // 摘要:
        //     Button 14 on sixth joystick.
        Joystick6Button14 = 464,
        //
        // 摘要:
        //     Button 15 on sixth joystick.
        Joystick6Button15 = 465,
        //
        // 摘要:
        //     Button 16 on sixth joystick.
        Joystick6Button16 = 466,
        //
        // 摘要:
        //     Button 17 on sixth joystick.
        Joystick6Button17 = 467,
        //
        // 摘要:
        //     Button 18 on sixth joystick.
        Joystick6Button18 = 468,
        //
        // 摘要:
        //     Button 19 on sixth joystick.
        Joystick6Button19 = 469,
        //
        // 摘要:
        //     Button 0 on seventh joystick.
        Joystick7Button0 = 470,
        //
        // 摘要:
        //     Button 1 on seventh joystick.
        Joystick7Button1 = 471,
        //
        // 摘要:
        //     Button 2 on seventh joystick.
        Joystick7Button2 = 472,
        //
        // 摘要:
        //     Button 3 on seventh joystick.
        Joystick7Button3 = 473,
        //
        // 摘要:
        //     Button 4 on seventh joystick.
        Joystick7Button4 = 474,
        //
        // 摘要:
        //     Button 5 on seventh joystick.
        Joystick7Button5 = 475,
        //
        // 摘要:
        //     Button 6 on seventh joystick.
        Joystick7Button6 = 476,
        //
        // 摘要:
        //     Button 7 on seventh joystick.
        Joystick7Button7 = 477,
        //
        // 摘要:
        //     Button 8 on seventh joystick.
        Joystick7Button8 = 478,
        //
        // 摘要:
        //     Button 9 on seventh joystick.
        Joystick7Button9 = 479,
        //
        // 摘要:
        //     Button 10 on seventh joystick.
        Joystick7Button10 = 480,
        //
        // 摘要:
        //     Button 11 on seventh joystick.
        Joystick7Button11 = 481,
        //
        // 摘要:
        //     Button 12 on seventh joystick.
        Joystick7Button12 = 482,
        //
        // 摘要:
        //     Button 13 on seventh joystick.
        Joystick7Button13 = 483,
        //
        // 摘要:
        //     Button 14 on seventh joystick.
        Joystick7Button14 = 484,
        //
        // 摘要:
        //     Button 15 on seventh joystick.
        Joystick7Button15 = 485,
        //
        // 摘要:
        //     Button 16 on seventh joystick.
        Joystick7Button16 = 486,
        //
        // 摘要:
        //     Button 17 on seventh joystick.
        Joystick7Button17 = 487,
        //
        // 摘要:
        //     Button 18 on seventh joystick.
        Joystick7Button18 = 488,
        //
        // 摘要:
        //     Button 19 on seventh joystick.
        Joystick7Button19 = 489,
        //
        // 摘要:
        //     Button 0 on eighth joystick.
        Joystick8Button0 = 490,
        //
        // 摘要:
        //     Button 1 on eighth joystick.
        Joystick8Button1 = 491,
        //
        // 摘要:
        //     Button 2 on eighth joystick.
        Joystick8Button2 = 492,
        //
        // 摘要:
        //     Button 3 on eighth joystick.
        Joystick8Button3 = 493,
        //
        // 摘要:
        //     Button 4 on eighth joystick.
        Joystick8Button4 = 494,
        //
        // 摘要:
        //     Button 5 on eighth joystick.
        Joystick8Button5 = 495,
        //
        // 摘要:
        //     Button 6 on eighth joystick.
        Joystick8Button6 = 496,
        //
        // 摘要:
        //     Button 7 on eighth joystick.
        Joystick8Button7 = 497,
        //
        // 摘要:
        //     Button 8 on eighth joystick.
        Joystick8Button8 = 498,
        //
        // 摘要:
        //     Button 9 on eighth joystick.
        Joystick8Button9 = 499,
        //
        // 摘要:
        //     Button 10 on eighth joystick.
        Joystick8Button10 = 500,
        //
        // 摘要:
        //     Button 11 on eighth joystick.
        Joystick8Button11 = 501,
        //
        // 摘要:
        //     Button 12 on eighth joystick.
        Joystick8Button12 = 502,
        //
        // 摘要:
        //     Button 13 on eighth joystick.
        Joystick8Button13 = 503,
        //
        // 摘要:
        //     Button 14 on eighth joystick.
        Joystick8Button14 = 504,
        //
        // 摘要:
        //     Button 15 on eighth joystick.
        Joystick8Button15 = 505,
        //
        // 摘要:
        //     Button 16 on eighth joystick.
        Joystick8Button16 = 506,
        //
        // 摘要:
        //     Button 17 on eighth joystick.
        Joystick8Button17 = 507,
        //
        // 摘要:
        //     Button 18 on eighth joystick.
        Joystick8Button18 = 508,
        //
        // 摘要:
        //     Button 19 on eighth joystick.
        Joystick8Button19 = 509
    }
}