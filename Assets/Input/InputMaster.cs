// GENERATED AUTOMATICALLY FROM 'Assets/Input/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace RPG_Project
{
    public class @InputMaster : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputMaster()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""PlayerMove"",
            ""id"": ""8296475f-9451-410b-a3a5-4f30b9b07eef"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""f6a649e5-4d9e-48d8-9d45-75da9e12c318"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""b52b98fa-8a5d-4058-9c9a-3533d5b644c6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""6074b139-178f-4690-b125-aefba54dd846"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""837072e7-f472-4ad5-9def-57c57e9906ac"",
                    ""path"": ""<Keyboard>/#(W)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6c204383-8b94-438f-8a9d-54100158ec16"",
                    ""path"": ""<Keyboard>/#(S)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6eec095f-549b-448a-84da-7c07578a8e2e"",
                    ""path"": ""<Keyboard>/#(A)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9e0abfdc-1211-494e-b723-46b47e07f6ac"",
                    ""path"": ""<Keyboard>/#(D)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""669e4215-5c8c-4479-bab8-e6e6bde41f87"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5a260673-b0e9-468d-94ab-d9d74f1de649"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pro Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c9ff2665-2712-4a59-ad77-b4c9728e43a7"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pro Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2bbb783b-a20d-4763-9dc4-bbe7dd75edc5"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pro Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""de528dbe-f537-4d27-a223-7b3c3cd6122f"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pro Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""09a385dc-d019-41c4-ae52-47355e86e32f"",
                    ""path"": ""<Keyboard>/#(J)"",
                    ""interactions"": ""Hold(duration=0.05)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ed36bbd-1d5c-482e-9ec7-0c4eb0e0a7b0"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Pro Controller"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerWeapon"",
            ""id"": ""34fc1c4e-79c4-467e-aee0-49e2982a1f27"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""dbd0e572-c910-4dea-95f4-6950581826b2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""4d4be599-1f87-4e87-837c-6d0865a52ba7"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6ea722b1-bd5a-4502-901a-e0bf1d5ab095"",
                    ""path"": ""<Keyboard>/#(W)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c4d45d84-6fed-4587-b556-d8f3df47708d"",
                    ""path"": ""<Keyboard>/#(S)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fe6e5b5d-3728-4ecd-be4a-60b37ac8a6ce"",
                    ""path"": ""<Keyboard>/#(A)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""96d76070-3e0d-4987-82e3-467ee46ee3b8"",
                    ""path"": ""<Keyboard>/#(D)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""ee4ee642-ea5f-4c7b-8b6b-8ed4eb3becfa"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""35113d66-8c7c-4ba8-930a-ce11477c08ac"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pro Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9314d2d7-c173-4fa4-9f78-f916771bef95"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pro Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1d63ce65-29bb-4e75-9538-2a84a691b26a"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pro Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""50c01dbf-37e4-4690-b9b8-0d538020fcaf"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pro Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Pro Controller"",
            ""bindingGroup"": ""Pro Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<SwitchProControllerHID>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // PlayerMove
            m_PlayerMove = asset.FindActionMap("PlayerMove", throwIfNotFound: true);
            m_PlayerMove_Movement = m_PlayerMove.FindAction("Movement", throwIfNotFound: true);
            m_PlayerMove_Run = m_PlayerMove.FindAction("Run", throwIfNotFound: true);
            // PlayerWeapon
            m_PlayerWeapon = asset.FindActionMap("PlayerWeapon", throwIfNotFound: true);
            m_PlayerWeapon_Movement = m_PlayerWeapon.FindAction("Movement", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // PlayerMove
        private readonly InputActionMap m_PlayerMove;
        private IPlayerMoveActions m_PlayerMoveActionsCallbackInterface;
        private readonly InputAction m_PlayerMove_Movement;
        private readonly InputAction m_PlayerMove_Run;
        public struct PlayerMoveActions
        {
            private @InputMaster m_Wrapper;
            public PlayerMoveActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
            public InputAction @Movement => m_Wrapper.m_PlayerMove_Movement;
            public InputAction @Run => m_Wrapper.m_PlayerMove_Run;
            public InputActionMap Get() { return m_Wrapper.m_PlayerMove; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerMoveActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerMoveActions instance)
            {
                if (m_Wrapper.m_PlayerMoveActionsCallbackInterface != null)
                {
                    @Movement.started -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnMovement;
                    @Run.started -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnRun;
                    @Run.performed -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnRun;
                    @Run.canceled -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnRun;
                }
                m_Wrapper.m_PlayerMoveActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                    @Run.started += instance.OnRun;
                    @Run.performed += instance.OnRun;
                    @Run.canceled += instance.OnRun;
                }
            }
        }
        public PlayerMoveActions @PlayerMove => new PlayerMoveActions(this);

        // PlayerWeapon
        private readonly InputActionMap m_PlayerWeapon;
        private IPlayerWeaponActions m_PlayerWeaponActionsCallbackInterface;
        private readonly InputAction m_PlayerWeapon_Movement;
        public struct PlayerWeaponActions
        {
            private @InputMaster m_Wrapper;
            public PlayerWeaponActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
            public InputAction @Movement => m_Wrapper.m_PlayerWeapon_Movement;
            public InputActionMap Get() { return m_Wrapper.m_PlayerWeapon; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerWeaponActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerWeaponActions instance)
            {
                if (m_Wrapper.m_PlayerWeaponActionsCallbackInterface != null)
                {
                    @Movement.started -= m_Wrapper.m_PlayerWeaponActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_PlayerWeaponActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_PlayerWeaponActionsCallbackInterface.OnMovement;
                }
                m_Wrapper.m_PlayerWeaponActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                }
            }
        }
        public PlayerWeaponActions @PlayerWeapon => new PlayerWeaponActions(this);
        private int m_KeyboardSchemeIndex = -1;
        public InputControlScheme KeyboardScheme
        {
            get
            {
                if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
                return asset.controlSchemes[m_KeyboardSchemeIndex];
            }
        }
        private int m_ProControllerSchemeIndex = -1;
        public InputControlScheme ProControllerScheme
        {
            get
            {
                if (m_ProControllerSchemeIndex == -1) m_ProControllerSchemeIndex = asset.FindControlSchemeIndex("Pro Controller");
                return asset.controlSchemes[m_ProControllerSchemeIndex];
            }
        }
        public interface IPlayerMoveActions
        {
            void OnMovement(InputAction.CallbackContext context);
            void OnRun(InputAction.CallbackContext context);
        }
        public interface IPlayerWeaponActions
        {
            void OnMovement(InputAction.CallbackContext context);
        }
    }
}
