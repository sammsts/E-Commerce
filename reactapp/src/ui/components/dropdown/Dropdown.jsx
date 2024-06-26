import { Menu, MenuButton, MenuItem, MenuItems, Transition } from '@headlessui/react'
import { ChevronDownIcon } from '@heroicons/react/20/solid'

function classNames(...classes) {
    return classes.filter(Boolean).join(' ')
}

export default function Dropdown() {
    return (
        <Menu as="div" className="relative inline-block text-left">
            <div>
                <MenuButton className="inline-flex w-full justify-center gap-x-1.5 rounded-md px-3 py-2 text-sm font-semibold text-gray-200 shadow-sm ring-1 ring-inset ring-gray-300 hover:bg-gray-50 hover:text-gray-800">
                    Departamentos
                    <ChevronDownIcon className="-mr-1 h-5 w-5 text-gray-400" aria-hidden="true" />
                </MenuButton>
            </div>

            <Transition
                enter="transition ease-out duration-100"
                enterFrom="transform opacity-0 scale-95"
                enterTo="transform opacity-100 scale-100"
                leave="transition ease-in duration-75"
                leaveFrom="transform opacity-100 scale-100"
                leaveTo="transform opacity-0 scale-95"
            >
                <MenuItems className="absolute right-0 z-10 mt-2 w-56 origin-top-right rounded-md bg-white shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none">
                    <div className="py-1">
                        <MenuItem>
                            {({ focus }) => (
                                <a
                                    href="/hardware"
                                    className={classNames(
                                        focus ? 'bg-gray-100 text-gray-900' : 'text-gray-700',
                                        'block px-4 py-2 text-sm',
                                    )}
                                >
                                    Hardware
                                </a>
                            )}
                        </MenuItem>
                        <MenuItem>
                            {({ focus }) => (
                                <a
                                    href="/periferico"
                                    className={classNames(
                                        focus ? 'bg-gray-100 text-gray-900' : 'text-gray-700',
                                        'block px-4 py-2 text-sm',
                                    )}
                                >
                                    Perif&eacute;ricos
                                </a>
                            )}
                        </MenuItem>
                        <MenuItem>
                            {({ focus }) => (
                                <a
                                    href="/computadores"
                                    className={classNames(
                                        focus ? 'bg-gray-100 text-gray-900' : 'text-gray-700',
                                        'block px-4 py-2 text-sm',
                                    )}
                                >
                                    Computadores
                                </a>
                            )}
                        </MenuItem>
                        <MenuItem>
                            {({ focus }) => (
                                <a
                                    href="/games"
                                    className={classNames(
                                        focus ? 'bg-gray-100 text-gray-900' : 'text-gray-700',
                                        'block px-4 py-2 text-sm',
                                    )}
                                >
                                    Games
                                </a>
                            )}
                        </MenuItem>
                    </div>
                </MenuItems>
            </Transition>
        </Menu>
    )
}
