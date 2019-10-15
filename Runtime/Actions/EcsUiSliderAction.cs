// ----------------------------------------------------------------------------
// The MIT License
// Ui extension https://github.com/Leopotam/ecs-ui
// for ECS framework https://github.com/Leopotam/ecs
// Copyright (c) 2017-2019 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using Leopotam.Ecs.Ui.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Leopotam.Ecs.Ui.Actions {
    /// <summary>
    /// Ui action for processing Slider events.
    /// </summary>
    [RequireComponent (typeof (Slider))]
    public sealed class EcsUiSliderAction : EcsUiActionBase {
        Slider _slider;

        void Awake () {
            _slider = GetComponent<Slider> ();
            _slider.onValueChanged.AddListener (OnSliderValueChanged);
        }

        void OnSliderValueChanged (float value) {
            if (IsValidForEvent ()) {
                var msg = Emitter.CreateMessage<EcsUiSliderChangeEvent> ();
                msg.WidgetName = WidgetName;
                msg.Sender = _slider;
                msg.Value = value;
            }
        }
    }
}