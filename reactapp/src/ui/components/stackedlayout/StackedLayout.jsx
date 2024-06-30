import {
    Disclosure,
    DisclosureButton,
    DisclosurePanel,
    Menu,
    MenuButton,
    MenuItem,
    MenuItems,
    Transition,
} from '@headlessui/react'
import { Bars3Icon, ShoppingCartIcon, XMarkIcon } from '@heroicons/react/24/outline'
import Dropdown from '../dropdown/Dropdown.jsx'
import ShoppingCart from '../shoppingcart/ShoppingCart.jsx'
import React, { useState, useEffect } from 'react';

const navigation = [
    { name: 'Pedidos', href: '/pedidos', current: false },
    { name: 'Categorias', href: '/categorias', current: false },
    { name: 'Ofertas do dia', href: '/ofertasdodia', current: false },
    { name: 'Suporte', href: '/contato', current: false },
]

function classNames(...classes) {
    return classes.filter(Boolean).join(' ')
}

export default function StackedLayout() {
    const [isShoppingCartOpen, setShoppingCartOpen] = useState(false);
    const [userNavigation, setUserNavigation] = useState([
        { name: 'Bem vindo(a) ', href: '#' },
        { name: 'Configura\u00e7\u00f5es', href: '/configuracoes' },
        { name: 'Sair', href: '/' },
    ]);
    const [user, setUser] = useState({
        name: '',
        email: '',
        imageUrl: ''
    });

    const toggleShoppingCart = () => {
        setShoppingCartOpen(prevState => !prevState);
    };

    const handleCloseShoppingCart = () => {
        setShoppingCartOpen(false);
    };

    const cleanBase64 = (base64String) => {
        return base64String.replace(/"/g, '');
    };

    useEffect(() => {
        const userName = sessionStorage.getItem('UserName');
        const userEmail = sessionStorage.getItem('UserEmail');
        const userImageUrl = sessionStorage.getItem('UserImgProfile');
        if (userName) {
            setUserNavigation(prevState => [
                { name: `Bem vindo(a) ${userName}`, href: '#' },
                ...prevState.slice(1)
            ]);
            setUser({
                name: userName,
                email: userEmail,
                imageUrl: userImageUrl
            });
        }
    }, []);

    return (
        <>
            <div className="min-h-full">
                <Disclosure as="nav" className="bg-gray-800">
                    {({ open }) => (
                        <>
                            <div className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
                                <div className="flex h-16 items-center justify-between">
                                    <div className="flex items-center">
                                        <div className="flex-shrink-0">
                                            <a href="/home">
                                                <img
                                                    className="h-8 w-8"
                                                    src="../dist/online-shop-ecommerce-svgrepo-com.png"
                                                    alt="Logo"
                                                />
                                            </a>
                                        </div>
                                        <div className="hidden md:block">
                                            <div className="ml-10 flex items-baseline space-x-4">
                                                <Dropdown />
                                                {navigation.map((item) => (
                                                    <a
                                                        key={item.name}
                                                        href={item.href}
                                                        className={classNames(
                                                            item.current
                                                                ? 'bg-gray-900 text-white'
                                                                : 'text-gray-300 hover:bg-gray-700 hover:text-white',
                                                            'rounded-md px-3 py-2 text-sm font-medium',
                                                        )}
                                                        aria-current={item.current ? 'page' : undefined}
                                                    >
                                                        {item.name}
                                                    </a>
                                                ))}
                                            </div>
                                        </div>
                                    </div>
                                    <div className="hidden md:block">
                                        <div className="ml-4 flex items-center md:ml-6">
                                            <button
                                                type="button"
                                                className="relative rounded-full bg-gray-800 p-1 text-gray-400 hover:text-white focus:outline-none focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800"
                                                onClick={ toggleShoppingCart }
                                            >
                                                <span className="absolute -inset-1.5" />
                                                <span className="sr-only">View notifications</span>
                                                <ShoppingCartIcon className="h-6 w-6" aria-hidden="true" />
                                            </button>

                                            { isShoppingCartOpen && (
                                                <ShoppingCart onClose={handleCloseShoppingCart} />
                                            )}

                                            {/* Profile dropdown */}
                                            <Menu as="div" className="relative ml-3">
                                                <div>
                                                    <MenuButton className="relative flex max-w-xs items-center rounded-full bg-gray-800 text-sm focus:outline-none focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800">
                                                        <span className="absolute -inset-1.5" />
                                                        <span className="sr-only">Open user menu</span>
                                                        <img className="h-10 w-10 rounded-full" src={`data:image/jpeg;base64,${cleanBase64(user.imageUrl) }`} alt="avatar" />
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
                                                    <MenuItems className="absolute right-0 z-10 mt-2 w-48 origin-top-right rounded-md bg-white py-1 shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none">
                                                        {userNavigation.map((item) => (
                                                            <MenuItem key={item.name}>
                                                                {
                                                                    item.name == 'Configura\u00e7\u00f5es' || item.name == 'Sair' ?
                                                                    ({ focus }) => (
                                                                        <a
                                                                            href={item.href}
                                                                            className={classNames(
                                                                                focus ? 'bg-gray-100' : '',
                                                                                'block px-4 py-2 text-sm text-gray-700',
                                                                            )}
                                                                        >
                                                                            {item.name}
                                                                        </a>
                                                                    ) :
                                                                    <div
                                                                        href={item.href}
                                                                        className={classNames(
                                                                            'block px-4 py-2 text-sm text-gray-700 hover:underline',
                                                                        )}
                                                                    >
                                                                        {item.name}
                                                                    </div>
                                                                }
                                                            </MenuItem>
                                                        ))}
                                                    </MenuItems>
                                                </Transition>
                                            </Menu>
                                        </div>
                                    </div>
                                    <div className="-mr-2 flex md:hidden">
                                        {/* Mobile menu button */}
                                        <DisclosureButton className="relative inline-flex items-center justify-center rounded-md bg-gray-800 p-2 text-gray-400 hover:bg-gray-700 hover:text-white focus:outline-none focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800">
                                            <span className="absolute -inset-0.5" />
                                            <span className="sr-only">Open main menu</span>
                                            {open ? (
                                                <XMarkIcon className="block h-6 w-6" aria-hidden="true" />
                                            ) : (
                                                <Bars3Icon className="block h-6 w-6" aria-hidden="true" />
                                            )}
                                        </DisclosureButton>
                                    </div>
                                </div>
                            </div>

                            <DisclosurePanel className="md:hidden">
                                <div className="space-y-1 px-2 pb-3 pt-2 sm:px-3">
                                    {navigation.map((item) => (
                                        <DisclosureButton
                                            key={item.name}
                                            as="a"
                                            href={item.href}
                                            className={classNames(
                                                item.current ? 'bg-gray-900 text-white' : 'text-gray-300 hover:bg-gray-700 hover:text-white',
                                                'block rounded-md px-3 py-2 text-base font-medium',
                                            )}
                                            aria-current={item.current ? 'page' : undefined}
                                        >
                                            {item.name}
                                        </DisclosureButton>
                                    ))}
                                </div>
                                <div className="border-t border-gray-700 pb-3 pt-4">
                                    <div className="flex items-center px-5">
                                        <div className="flex-shrink-0">
                                            <img className="h-10 w-10 rounded-full" src={user.imageUrl} alt="" />
                                        </div>
                                        <div className="ml-3">
                                            <div className="text-base font-medium leading-none text-white">{user.name}</div>
                                            <div className="text-sm font-medium leading-none text-gray-400">{user.email}</div>
                                        </div>
                                        <button
                                            type="button"
                                            className="relative ml-auto flex-shrink-0 rounded-full bg-gray-800 p-1 text-gray-400 hover:text-white focus:outline-none focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800"
                                        >
                                            <span className="absolute -inset-1.5" />
                                            <span className="sr-only">View notifications</span>
                                            <ShoppingCartIcon className="h-6 w-6" aria-hidden="true" />
                                        </button>
                                    </div>
                                    <div className="mt-3 space-y-1 px-2">
                                        {userNavigation.map((item) => (
                                            <DisclosureButton
                                                key={item.name}
                                                as="a"
                                                href={item.href}
                                                className="block rounded-md px-3 py-2 text-base font-medium text-gray-400 hover:bg-gray-700 hover:text-white"
                                            >
                                                {item.name}
                                            </DisclosureButton>
                                        ))}
                                    </div>
                                </div>
                            </DisclosurePanel>
                        </>
                    )}
                </Disclosure>
            </div>
        </>
    )
}
